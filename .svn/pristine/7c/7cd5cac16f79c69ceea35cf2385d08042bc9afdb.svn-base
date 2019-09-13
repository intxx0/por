using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKor.InfraWeb
{
 
    public class PaginacaoConfig<T>
    {
        public int Page { get; set; }

        public int Count
        {
            get
            {
                return (null != this.Items) ? this.Items.Count() : 0;
            }
        }

        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public int IdUsuarioLogado { get; set; }
        public IEnumerable<T> Items { get; set; }
        
    }
}