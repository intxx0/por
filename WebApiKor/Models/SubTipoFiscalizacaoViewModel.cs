using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKor.Models
{
    public class SubTipoFiscalizacaoViewModel
    {
        public int IdSubTipoFiscalizacao { set; get; }
        public int IdTipoFiscalizacao { set; get; }
        public string DescSubTipoFiscalizacao { set; get; }
        public string DescTipoFiscalizacao { set; get; }
    }
}