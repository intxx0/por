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
    
    public partial class usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuario()
        {
            this.caixa_email = new HashSet<caixa_email>();
            this.emergencia = new HashSet<emergencia>();
            this.emergencia1 = new HashSet<emergencia>();
            this.imagem = new HashSet<imagem>();
            this.mensagem = new HashSet<mensagem>();
            this.mensagem1 = new HashSet<mensagem>();
            this.operacao = new HashSet<operacao>();
            this.usuario_logado = new HashSet<usuario_logado>();
            this.usuario_operacao = new HashSet<usuario_operacao>();
        }
    
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
        public Nullable<System.DateTime> data_posicao { get; set; }
        public Nullable<System.DateTime> data_criado { get; set; }
        public Nullable<System.DateTime> ultimo_acesso { get; set; }
        public Nullable<int> qtd_acessos { get; set; }
        public Nullable<int> ativo { get; set; }
        public Nullable<int> deleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<caixa_email> caixa_email { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<emergencia> emergencia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<emergencia> emergencia1 { get; set; }
        public virtual grupo grupo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<imagem> imagem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mensagem> mensagem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mensagem> mensagem1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<operacao> operacao { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<usuario_logado> usuario_logado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<usuario_operacao> usuario_operacao { get; set; }
    }
}