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
    
    public partial class Telegrama
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Telegrama()
        {
            this.Outros = new HashSet<Outros>();
            this.Rcms = new HashSet<Rcms>();
            this.Rdm = new HashSet<Rdm>();
        }
    
        public int IdTelegrama { get; set; }
        public string NomeProjeto { get; set; }
        public decimal ValorAprovado { get; set; }
        public double ParticipacaoDr { get; set; }
        public double ParticipacaoDn { get; set; }
        public System.DateTime DataAutorizacao { get; set; }
        public System.DateTime DataLimite { get; set; }
        public string Finalidade { get; set; }
        public string CodTelegrama { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Outros> Outros { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rcms> Rcms { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rdm> Rdm { get; set; }
    }
}
