using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKor.Models
{
    public class GuiaViewModel
    {
        public int IdGuia { get; set; }

        public string NumeroGuia { get; set; }

        public string NomeEmisssor { get; set; }

        public string CnpjEmissor { get; set; }

        public string MunicipioEmissor { get; set; }

        public string NomeOrigem { get; set; }

        public string MunicipioOrigem { get; set; }

        public string NomeDestino { get; set; }

        public string MunicipioDestino { get; set; }

        public string PlacaRegistro { get; set; }

        public string NumeroNotaFiscal { get; set; }

        public DateTime DataEmissaoGuia { get; set; }

        public DateTime DataValidadeInicio { get; set; }

        public DateTime DataValidadeFim { get; set; }

        public int Quantidade { get; set; }

        public string UnidadeMedida { get; set; }

        public decimal Valor { get; set; }

        public int Ativo { get; set; }

    }
}