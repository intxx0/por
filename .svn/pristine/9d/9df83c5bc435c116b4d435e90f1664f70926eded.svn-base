using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesKor.Entidades
{
    public class grupo
    {
        public grupo()
        {
            this.usuario = new List<usuario>();
        }

        [Key]
        public int grupo_id { get; set; }
        public string nome { get; set; }
        public DateTime? datacriado { get; set; }
        public int? ativo { get; set; }
        public DateTime datamodificado { get; set; }

        public virtual ICollection<usuario> usuario { get; set; }
    }
}
