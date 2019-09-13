using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKor.Models
{
    public class EmailViewModel
    {
        public int CaixaEmailId { get; set; }
        public int UsuarioId { get; set; }
        public string Destinatario { get; set; }
        public string Remetente { get; set; }
        public string Copia { get; set; }
        public string Assunto { get; set; }
        public string Texto { get; set; }
        public string Tipo { get; set; }
        public int Baixado { get; set; }
        public int Deletado { get; set; }
        public DateTime DataCriado { get; set; }
        public int Enviado { get; set; }
        //public List<email> Para { get; set; }
        public string Para { get; set; }
        public int Ativo { get; set; }
        public bool Visualizado { get; set; }
    }


    public class email
    {
        public string text { get; set; }
    }


}