using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKor.Models
{
    public class GruposViewModel
    {
        public int IdGrupo { get; set; }

        public string DescGrupo { get; set; }

        public DateTime DataCriado { get; set; }
        public DateTime DataModificado { get; set; }
        public int Ativo { get; set; }
    }
}