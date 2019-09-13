using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesKor.Entidades
{
    public class guia
    {
        public guia()
        {
            item_guia = new List<item_guia>();
        }

        [Key]
        public int id_guia { get; set; }
        public string numero_guia { get; set; }
        public string nome_emissor { get; set; }
        public string cnpj_emissor { get; set; }
        public string municipio_emissor { get; set; }
        public string nome_origem { get; set; }
        public string municipio_origem { get; set; }
        public string nome_destino { get; set; }
        public string municipio_destino { get; set; }
        public string placa_registro { get; set; }
        public string numero_notafiscal { get; set; }
        public DateTime? data_emissao_guia { get; set; }
        public DateTime? data_validade_inicio { get; set; }
        public DateTime? data_validade_fim { get; set; }
        public int? ativo { get; set; }

        public virtual ICollection<item_guia> item_guia { get; set; }

    }
}
