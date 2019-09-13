using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKor.Models
{
    public class ContaEmailViewModel
    {
        public int IdContaEmail { get; set; }
        public int UsuarioId { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public int SmtpAuth { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string Pop3Host { get; set; }
        public int Pop3Port { get; set; }
        public string Pop3Username { get; set; }
        public string Pop3Password { get; set; }
        public int Pop3Delete { get; set; }
        public string NomeUsuario { get; set; }
    }
}