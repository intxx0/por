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
    
    public partial class evento
    {
        public int evento_id { get; set; }
        public Nullable<int> tipo_evento_id { get; set; }
        public Nullable<int> usuario_id { get; set; }
        public Nullable<System.DateTime> datacriado { get; set; }
        public string coordenada { get; set; }
        public Nullable<int> usuario_id_visualizado { get; set; }
        public Nullable<int> id_fiscalizacao { get; set; }
        public Nullable<int> id_kor { get; set; }
        public Nullable<int> id_tipo_fiscalizacao { get; set; }
    
        public virtual fiscalizacao fiscalizacao { get; set; }
        public virtual tipo_evento tipo_evento { get; set; }
        public virtual usuario usuario { get; set; }
        public virtual usuario usuario1 { get; set; }
        public virtual kor kor { get; set; }
        public virtual tipo_fiscalizacao tipo_fiscalizacao { get; set; }
    }
}
