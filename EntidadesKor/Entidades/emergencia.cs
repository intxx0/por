using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesKor.Entidades
{
    public class emergencia
    {
        public emergencia()
        {
            usuarios = new List<usuario>();
        }
        
        [Key]
        public int emergencia_id { get; set; }
        public int? usuario_id { get; set; }
        public usuario usuario_atendeu_id { get; set; }
        public string coordenada { get; set; }
        public DateTime? datacriado { get; set; }
        public int? deleted { get; set; }
        public int? ativo { get; set; }
        public virtual ICollection<usuario> usuarios { get; set; }
        public virtual usuario usuario { get; set; }
    }
}
