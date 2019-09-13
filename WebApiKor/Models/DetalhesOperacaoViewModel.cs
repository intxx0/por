using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKor.Models
{
    public class DetalhesOperacaoViewModel
    {
        public Object operacao { get; set; }

        public Object fiscalizacoes { get; set; }

        public Object kor { get; set; }

        public Object usuarios { get; set; }
    }
}