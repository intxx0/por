using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesKor.Entidades
{
    public class tipo_materia_prima

    {
        public tipo_materia_prima()
        {
            item_guia = new List<item_guia>();
        }

        [Key]
        public int id_tipo_materia_prima { get; set; }

        public string desc_tipo_materia_prima { get; set; }

        public int? ativo { get; set; }

        public virtual ICollection<item_guia> item_guia { get; set; }
    }
}
