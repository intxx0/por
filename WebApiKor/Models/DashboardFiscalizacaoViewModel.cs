using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKor.Models
{
    public class DashboardFiscalizacaoViewModel
    {
        public int IdOperacao { get; set; }

        public int IdFiscalizacao { get; set; }

        public string DataFiscalizacao { get; set; }

        public int IdStatusFiscalizacao { get; set; }

        public string StatusFiscalizacao { get; set; }

      

        public int QtdFiscalizacoes { get; set; }

    }


    public class DashboardTipoMateriaPrimaViewModel
    {

        public int IdFiscalizacao { get; set; }

        public string DescMateriaPrima { get; set; }

        public int QtdAutuacoesMateriaPrima { get; set; }
    }


    public class DashboardAutuadasOrigemViewModel
    {
        public int IdFiscalizacao { get; set; }

        public string DescAutudasOrigem { get; set; }

        public int QtdAutuacoesOrigem { get; set; }
    }

}