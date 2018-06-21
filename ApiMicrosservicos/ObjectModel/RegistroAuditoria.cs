using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Api.ObjectModel
{
    public class RegistroAuditoria
    {
        public bool IsAuthenticated { get; set; }
        public UsuarioLogin Usuario { get; set; }
        public string CaminhoRequest { get; set; }
        public string BodyContent { get; set; }
        public string QueryString { get; set; }
    }
}
