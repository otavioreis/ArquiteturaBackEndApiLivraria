using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livraria.Api.Middleware;
using Livraria.Api.Repository;
using Livraria.Api.Repository.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.Swagger;

namespace Livraria.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });



            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "API de Livraria",
                    Description = "Um exemplo de endpoints de uma API de uma loja de livraria construída com ASP.NET Core Web API",
                    Contact = new Contact
                    {
                        Name = "Otávio Augusto de Queiroz Reis",
                        Email = "otavioreis@gmail.com",
                        Url = String.Empty
                    },
                    License = new License
                    {
                        Name = "Licença MIT",
                        Url = "https://raw.githubusercontent.com/otavioreis/ArquiteturaBackEndApiLivraria/master/LICENSE"
                    }
                });
                c.IncludeXmlComments(GetXmlCommentsPath());
                c.OperationFilter<ExamplesOperationFilter>(); // [SwaggerRequestExample] & [SwaggerResponseExample]
                c.OperationFilter<DescriptionOperationFilter>(); // [Description] on Response properties

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "Autorização utilizando JWT header usando o esquema Bearer. Examplo: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);
            });

            services.AddMvc();
            services.AddSession();
            services.AddSingleton<ILivrariaRepository, LivrariaRepository>();
            services.AddSingleton<IPedidoRepository, PedidoRepository>();
        }

        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}/Swagger.xml", System.AppDomain.CurrentDomain.BaseDirectory);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Livraria V1");
            });

            app.UseAuditoriaMiddleware();
            app.UseSession();
            app.UseMvc();
        }


    }
}
