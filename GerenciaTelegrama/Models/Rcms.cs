//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GerenciaTelegrama.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rcms
    {
        public int IdRCMS { get; set; }
        public string Area { get; set; }
        public string Finalidade { get; set; }
        public System.DateTime Data { get; set; }
        public int IdTelegrama { get; set; }
        public string CodRcms { get; set; }
    
        public virtual Telegrama Telegrama { get; set; }
    }
}
