using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKor.Models
{
    public class FiscalizacaoOnLineViewModel
    {
        
        public int IdFiscalizacao { get; set; }

        public int IdUsuario { get; set; }

        public string NomeUsuario { get; set; }

        public string DescObservacao { get; set; }

        public string DescOperacao { get; set; }

        public string NomeResponsavel { get; set; }

        public string CargoResponsavel { get; set; }

        public string InstituicaoResponsavel { get; set; }
        
        public string EmailResposnsavel { get; set; }

        public string TelResponsavel { get; set; }

        public int IdNatureza { get; set; }

        public string DescNaturezaOperacao { get; set; }

        public DateTime DataCriado { get; set; }

        public DateTime DataFinalOperacao { get; set; }

        public List<tipo_fiscalizacao> tipoFiscalizacao { get; set; }

     public int Ativo { get; set; }
        
    }
}