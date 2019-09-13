﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKor.Models
{
    public class FiscalizacaoViewModel
    {
        public int IdFiscalizacao { set; get; }
        public string DescFiscalizacao { set; get; }
        public DateTime DataFiscalizacao { set; get; }
        public string Posto { set; get; }
        public int IdStatusFiscalizacao { set; get; }
        public int IdOperacao { set; get; }
        public int IdGuia { set; get; }
        public int Ativo { set; get; }
        public int IdSubTipoFiscalizacao { set; get; }
        public bool Finalizada { set; get; }
        public int UsuarioId { set; get; }
        public DateTime DataGravacao { set; get; }
        public string NumeroAutuacao { set; get; }
        public int IdKor { set; get; }
        public int Sincronizado { set; get; }
    }
}