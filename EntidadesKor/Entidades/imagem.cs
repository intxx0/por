using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesKor.Entidades
{
    public class imagem
    {
        [Key]
        public int imagem_id { get; set; }
        public string nome { get; set; }
        public int? usuario_id { get; set; }
        public int? id_fiscalizacao { get; set; }
        public DateTime? datacriado { get; set; }
        public int? ativo { get; set; }

        public virtual fiscalizacao fiscalizacao { get; set; }
        public virtual usuario usuario { get; set; }
    }
}
