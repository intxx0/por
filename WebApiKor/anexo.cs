//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApiKor
{
    using System;
    using System.Collections.Generic;
    
    public partial class anexo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public anexo()
        {
            this.anexo_caixa_email = new HashSet<anexo_caixa_email>();
            this.anexo_mensagem = new HashSet<anexo_mensagem>();
            this.anexo_fiscalizacao = new HashSet<anexo_fiscalizacao>();
        }
    
        public int id_anexo { get; set; }
        public string nome { get; set; }
        public Nullable<System.DateTime> datacriado { get; set; }
        public Nullable<int> ativo { get; set; }
        public Nullable<int> usuario_id { get; set; }
        public string desc_anexo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<anexo_caixa_email> anexo_caixa_email { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<anexo_mensagem> anexo_mensagem { get; set; }
        public virtual usuario usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<anexo_fiscalizacao> anexo_fiscalizacao { get; set; }
    }
}