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
    
    public partial class usuario_logado
    {
        public int usuario_logado_id { get; set; }
        public int usuario_id { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    
        public virtual usuario usuario { get; set; }
    }
}