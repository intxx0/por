using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesKor.Entidades
{
    public class operacao
    {
        public operacao()
        {
            fiscalizacao = new List<fiscalizacao>();
        }

        [Key]
        public int id_operacao { get; set; }
        public string desc_operacao { get; set; }
        public DateTime? data_criado_operacao { get; set; }
        public DateTime? data_final_operacao { get; set; }
        public int? ativo { get; set; }
        public int? usuario_id { get; set; }

        public virtual ICollection<fiscalizacao> fiscalizacao { get; set; }
        public virtual usuario usuario { get; set; }
    }
}
