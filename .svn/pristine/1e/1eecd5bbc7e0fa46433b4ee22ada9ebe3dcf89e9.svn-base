using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesKor.Entidades
{
    public class item_guia
    {
        [Key]
        public int id_item_guia { get; set; }
        public int? id_tipo_materia_prima { get; set; }
        public string descricao_item { get; set; }
        public int? id_guia { get; set; }
        public int? qtd_item { get; set; }
        public string unidade_item { get; set; }
        public decimal? valor_item { get; set; }
        public int? ativo { get; set; }

        public virtual guia guia { get; set; }
        public virtual tipo_materia_prima tipo_materia_prima { get; set; }
    }
}
