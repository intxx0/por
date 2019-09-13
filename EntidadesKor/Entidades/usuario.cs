using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace EntidadesKor.Entidades
{
   public class usuario
    {

        public usuario()
        {
            caixa_email = new List<caixa_email>();
           imagem = new List<imagem>();
            mensagem = new List<mensagem>();
            operacao = new List<operacao>();
        }

        [Key]
        public int usuario_id { get; set; }
        public int grupo_id { get; set; }
        public string nome { get; set; }
        public string identificador { get; set; }
        public string login { get; set; }
        public string email { get; set; }
        public string imap_server { get; set; }
        public string email_senha { get; set; }
        public string senha { get; set; }
        public string coordenada { get; set; }
        public DateTime data_posicao { get; set; }
        public DateTime data_criado { get; set; }
        public DateTime ultimo_acesso { get; set; }
        public int qtd_acessos { get; set; }
        public int ativo { get; set; }
        public int deleted { get; set; }

        public virtual ICollection<caixa_email> caixa_email { get; set; }
        public virtual grupo grupo { get; set; }
        public virtual ICollection<imagem> imagem { get; set; }
        public virtual ICollection<mensagem> mensagem { get; set; }
        public virtual ICollection<operacao> operacao { get; set; }

    }
}
