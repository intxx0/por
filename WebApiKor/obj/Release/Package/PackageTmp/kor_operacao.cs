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
    
    public partial class kor_operacao
    {
        public int id_kor_operacao { get; set; }
        public int id_kor { get; set; }
        public int id_operacao { get; set; }
    
        public virtual kor kor { get; set; }
    }
}