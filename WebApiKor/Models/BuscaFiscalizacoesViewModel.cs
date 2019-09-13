﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKor.Models
{
    public class BuscaFiscalizacoesViewModel
    {
        public int? IdNaturezaOperacao { set; get; }
        public int? IdKor { set; get; }
        public int? IdOperacao { set; get; }
        public int? IdInstituicao { set; get; }
        public int? IdUsuario { get; set; }
        public int? IdTipoFiscalizacao { set; get; }
        public int? IdSubTipoFiscalizacao { set; get; }
        public string DataInicio { set; get; }
        public string DataTermino { set; get; }
    }
}