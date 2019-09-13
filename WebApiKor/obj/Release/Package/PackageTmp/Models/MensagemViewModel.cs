using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKor.Models
{
    public class MensagemViewModel
    {
        public int IdMensagem { get; set; }

        public int IdUsuario { get; set; }

        public string NomeRemetente { get; set; }

        public int UsuarioDestino { get; set; }

        public string NomeDestinatario { get; set; }

        public string TextoMensagem { get; set; }

        public int Visualizado { get; set; }

        public int Baixado { get; set; }

        public DateTime DataCriado { get; set; }

        public DateTime DataModificado { get; set; }

        public int Deletado { get; set; }

    }
}