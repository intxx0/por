using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKor.Models
{
    public class UsuarioViewModel
    {
        public int UsuarioId { get; set; }
        public int IdGrupo { get; set; }
        public string DescLogin { get; set; }
        public string Idenficador { get; set; }
        public string Coordenada { get; set; }
        public byte Deletado { get; set; }
        public string Senha { get; set; }
        public string DescGrupo { get; set; }
        public string DescNome { get; set; }
        public string Email { get; set; }
        public DateTime UltimoAcesso { get; set; }
        public int QtdAcessos { get; set; }
        public string ImapServer { get; set; }
        public string EmailSenha { get; set; }
        public DateTime DataCriado { get; set; }
        public string Ativo { get; set; }
        public int IdCargo { set; get; }
        public string Tel { set; get; }
        public int IdInstituicao { set; get; }        
    }
}