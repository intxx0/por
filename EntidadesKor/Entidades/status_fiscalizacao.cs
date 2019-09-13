using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesKor.Entidades
{
    public class status_fiscalizacao
    {

        public status_fiscalizacao()
        {
            fiscalizacaos = new List<fiscalizacao>();
        }

        [Key]
        public int id_status_fiscalizacao { get; set; }

        public string desc_status_fiscalizacao { get; set; }

        public virtual ICollection<fiscalizacao> fiscalizacaos { get; set; }
    }
}
