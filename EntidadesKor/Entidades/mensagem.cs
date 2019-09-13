using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesKor.Entidades
{
    public class mensagem
    {
        [Key]
        public int mensagem_id { get; set; }
        public int? usuario_id { get; set; }
        public int? usuario_destino_id { get; set; }
        public string texto { get; set; }
        public int? visualizado { get; set; }
        public int? baixado { get; set; }
        public DateTime? datacriado { get; set; }
        public DateTime? datamodificado { get; set; }
        public int? deleted { get; set; }

        public virtual usuario usuarios { get; set; }
    }
}
