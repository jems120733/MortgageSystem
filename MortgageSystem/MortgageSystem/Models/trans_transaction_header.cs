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
    
    public partial class trans_transaction_header
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public trans_transaction_header()
        {
            this.crm_mortgage_daily_payables = new HashSet<crm_mortgage_daily_payables>();
            this.trans_payment_collection = new HashSet<trans_payment_collection>();
            this.trans_transaction_detail = new HashSet<trans_transaction_detail>();
        }
    
        public long id { get; set; }
        public int trans_transaction_type_id { get; set; }
        public System.DateTime sales_date { get; set; }
        public System.DateTime date_created { get; set; }
        public int mf_document_type_id { get; set; }
        public int crm_branch_id { get; set; }
        public Nullable<int> crm_from_branch_id { get; set; }
        public Nullable<int> crm_to_branch_id { get; set; }
        public Nullable<long> crm_customer_id { get; set; }
        public int crm_user_id { get; set; }
        public Nullable<int> crm_admin_id { get; set; }
        public Nullable<int> inv_discount_id { get; set; }
        public Nullable<int> mf_is_void_status_id { get; set; }
        public Nullable<int> mf_open_status_id { get; set; }
        public Nullable<decimal> discount_amount { get; set; }
        public string comment { get; set; }
    
        public virtual crm_branch crm_branch { get; set; }
        public virtual crm_branch crm_branch1 { get; set; }
        public virtual crm_branch crm_branch2 { get; set; }
        public virtual crm_customer crm_customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<crm_mortgage_daily_payables> crm_mortgage_daily_payables { get; set; }
        public virtual crm_user crm_user { get; set; }
        public virtual crm_user crm_user1 { get; set; }
        public virtual inv_discount inv_discount { get; set; }
        public virtual mf_document_type mf_document_type { get; set; }
        public virtual mf_status mf_status { get; set; }
        public virtual mf_status mf_status1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<trans_payment_collection> trans_payment_collection { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<trans_transaction_detail> trans_transaction_detail { get; set; }
        public virtual trans_transaction_type trans_transaction_type { get; set; }
    }
}
