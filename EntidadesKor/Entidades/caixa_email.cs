using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesKor.Entidades
{
   public class caixa_email
    {

        [Key]
        public int caixa_email_id { get; set; }
        public int? usuario_id { get; set; }
        public string destinatario { get; set; }
        public string remetente { get; set; }
        public string assunto { get; set; }
        public string texto { get; set; }
        public string tipo { get; set; }
        public int? ativo { get; set; }
        public int? enviado { get; set; }
        public int? baixado { get; set; }
        public DateTime? datacriado { get; set; }
        public DateTime? deleted { get; set; }

        public virtual usuario usuario { get; set; }
    }
}
