using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKor.Models
{
    public class EventoViewModel
    {
        public int EventoId { get; set; }
        public int usuarioIdVisualizado { get; set; }
        public String NumeroAutuacao { get; set; }
        public String TipoEvento { get; set; }
        public String NomeUsuario { get; set; }
        public DateTime DataCriado { get; set; }
        public String Coordenada { get; set; }
        public String NomeUsuarioVisualizado { get; set; }
        public int IdFiscalizacao{ get; set; }
        public String StatusFisc { get; set; }
        public String NomeKor { get; set; }
        public String DescFisc { get; set; }
    }
}