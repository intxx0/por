using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKor.Models
{
    public class ResultadoBuscaFiscalizacaoViewModel
    {
        public string DescOperacao { set; get; }
        public string GuiaEmissor { set; get; }
        public string GuiaNumero { set; get; }
        public string DescFiscalizacao { set; get; }
        public string DataFiscalizacao { set; get; }
    }
}