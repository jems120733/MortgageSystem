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
    
    public partial class inv_item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public inv_item()
        {
            this.inv_pricebook_detail = new HashSet<inv_pricebook_detail>();
            this.trans_transaction_detail = new HashSet<trans_transaction_detail>();
        }
    
        public int id { get; set; }
        public string short_description { get; set; }
        public string full_description { get; set; }
        public int status_id { get; set; }
        public Nullable<int> branch_id { get; set; }
    
        public virtual crm_branch crm_branch { get; set; }
        public virtual mf_status mf_status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<inv_pricebook_detail> inv_pricebook_detail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<trans_transaction_detail> trans_transaction_detail { get; set; }
    }
}
