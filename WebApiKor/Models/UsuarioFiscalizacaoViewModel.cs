using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKor.Models
{
    public class UsuarioOperacaoViewModel
    {
        public int IdUsuarioOperacao { get; set; }
        
        public int IdUsuario { get; set; }
        
        public int IdOperacao { get; set; }

        public string DescNomeUsuario { get; set; }

        public int IdGrupo { get; set; }

        public string DescLoginUsuario { get; set; }

        public string DescEmailUsuario { get; set; }

        public string EmailSenha { get; set; }

        public string Senha { get; set; }

        public string SmtpEmail { get; set; }

        public int Ativo { get; set; }

    }
}