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
    
    public partial class crm_branch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public crm_branch()
        {
            this.crm_requirements_tf = new HashSet<crm_requirements_tf>();
            this.trans_payment_collection = new HashSet<trans_payment_collection>();
            this.trans_transaction_header = new HashSet<trans_transaction_header>();
            this.trans_transaction_header1 = new HashSet<trans_transaction_header>();
            this.trans_transaction_header2 = new HashSet<trans_transaction_header>();
            this.crm_assignment = new HashSet<crm_assignment>();
            this.inv_item = new HashSet<inv_item>();
            this.inv_pricebook_header = new HashSet<inv_pricebook_header>();
        }
    
        public int id { get; set; }
        public string description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<crm_requirements_tf> crm_requirements_tf { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<trans_payment_collection> trans_payment_collection { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<trans_transaction_header> trans_transaction_header { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<trans_transaction_header> trans_transaction_header1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<trans_transaction_header> trans_transaction_header2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<crm_assignment> crm_assignment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<inv_item> inv_item { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<inv_pricebook_header> inv_pricebook_header { get; set; }
    }
}
