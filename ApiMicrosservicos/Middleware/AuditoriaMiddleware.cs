using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Livraria.Api.ApiClient;
using Livraria.Api.ObjectModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Newtonsoft.Json;

namespace Livraria.Api.Middleware
{
    public class AuditoriaMiddleware
    {
        private readonly RequestDelegate _next;
        private HttpClient _httpClient;

        public AuditoriaMiddleware(RequestDelegate next)
        {
            _next = next;
            _httpClient = new HttpClient();
        }

        public async Task Invoke(HttpContext context)
        {
            var apiKey = ((FrameRequestHeaders)context.Request.Headers).HeaderAuthorization;
            var apiHttpClient = new ApiHttpClient(apiKey, "http://localhost:61000/", _httpClient);


            var userIsAuthenticated = context.User.Identity.IsAuthenticated;
            var req = context.Request;
            string bodyContent;
            var queryString = req.QueryString.ToString();

            // Allows using several time the stream in ASP.Net Core
            req.EnableRewind();

            using (StreamReader reader
                = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyContent = reader.ReadToEnd();
            }

            // Rewind, so the core is not lost when it looks the body for the request
            req.Body.Position = 0;

            UsuarioLogin usuarioLogin = null;

            if (userIsAuthenticated)
            {
                usuarioLogin = new UsuarioLogin
                {
                    Nome = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                    AuthTime = Convert.ToDateTime(context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.AuthTime)?.Value, CultureInfo.InvariantCulture)
                };
            }

            var caminhoRequest = context.Request.Path;


            var registroAuditoria = new RegistroAuditoria
            {
                IsAuthenticated = userIsAuthenticated,
                Usuario = usuarioLogin,
                BodyContent = bodyContent,
                CaminhoRequest = caminhoRequest,
                QueryString = queryString
            };

            var resposta = await apiHttpClient.HttpClient.PostAsync("v1/private/auditoria", JsonConvert.SerializeObject(registroAuditoria));

            await _next.Invoke(context);


            // Clean up.
        }
    }

    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuditoriaMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuditoriaMiddleware>();
        }
    }
}
