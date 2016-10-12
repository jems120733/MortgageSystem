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
    
    public partial class trans_payment_collection
    {
        public long id { get; set; }
        public long trans_transaction_header_id { get; set; }
        public int mf_payment_type_id { get; set; }
        public int crm_user_id { get; set; }
        public Nullable<int> crm_admin_id { get; set; }
        public Nullable<long> crm_collector_id { get; set; }
        public Nullable<int> crm_branch_id { get; set; }
        public Nullable<int> mf_status_id { get; set; }
        public decimal amount { get; set; }
        public decimal open_balance_amount { get; set; }
        public System.DateTime payment_date { get; set; }
        public System.DateTime sales_date { get; set; }
        public string comment { get; set; }
        public Nullable<decimal> discount_amount { get; set; }
        public Nullable<decimal> penalty_amount { get; set; }
    
        public virtual crm_branch crm_branch { get; set; }
        public virtual crm_employee crm_employee { get; set; }
        public virtual crm_user crm_user { get; set; }
        public virtual crm_user crm_user1 { get; set; }
        public virtual mf_payment_type mf_payment_type { get; set; }
        public virtual mf_status mf_status { get; set; }
        public virtual trans_transaction_header trans_transaction_header { get; set; }
    }
}
