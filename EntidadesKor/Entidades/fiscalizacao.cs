using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesKor.Entidades
{
    public class fiscalizacao
    {
    
            public fiscalizacao()
            {
                imagem = new List<imagem>();
            }

            [Key]
            public int id_fiscalizacao { get; set; }

            public DateTime? data_fiscalizacao { get; set; }
            public string posto { get; set; }
            public string numero_guia { get; set; }
            public int? qtd_verificada { get; set; }
            public string placa_registro { get; set; }
            public string nota_fiscal { get; set; }
            public string validade_documento { get; set; }
            public int? id_status_fiscalizacao { get; set; }
            public int? id_operacao { get; set; }
            public int? ativo { get; set; }

            public virtual operacao operacao { get; set; }
            public virtual status_fiscalizacao status_fiscalizacao { get; set; }
            public virtual ICollection<imagem> imagem { get; set; }
        }
    }

