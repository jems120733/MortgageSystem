//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MortgageSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class inv_pricebook_header
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public inv_pricebook_header()
        {
            this.inv_pricebook_detail = new HashSet<inv_pricebook_detail>();
        }
    
        public int id { get; set; }
        public string description { get; set; }
        public Nullable<int> branch_id { get; set; }
        public Nullable<long> customer_id { get; set; }
        public int pricebook_type_id { get; set; }
        public Nullable<int> status_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<inv_pricebook_detail> inv_pricebook_detail { get; set; }
        public virtual crm_branch crm_branch { get; set; }
        public virtual crm_customer crm_customer { get; set; }
        public virtual inv_pricebook_type inv_pricebook_type { get; set; }
        public virtual mf_status mf_status { get; set; }
    }
}
