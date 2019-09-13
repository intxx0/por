using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKor.Models
{
    public class TipoFiscalizacaoViewModel
    {
        public int IdTipoFiscalizacao { get; set; }
        public int IdNaturezaOperacao { get; set; }
        public string DescTipoFiscalizacao { get; set; }
        public int Ativo { get; set; }
    }

}