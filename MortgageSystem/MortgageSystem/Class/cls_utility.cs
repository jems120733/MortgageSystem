using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MortgageSystem.Models;
using MortgageSystem.Class;

namespace MortgageSystem.Class
{
    public class cls_utility
    {
        static mortgageEntities db = new mortgageEntities();
        public static Int64 cls_header_id;
        public static void add_transaction_header(int trans_type_id, DateTime sales_date, DateTime date_created, int doctype_id, int branch_id,
                            int? from_branch_id, int? to_branch_id, Int64? customer_id, int user_id, int? admin_id, int? discount_id,
                            int? is_void_status_id, int? open_status_id, decimal? discount_amount, string comment)
        {
            trans_transaction_header th = new trans_transaction_header();
            th.trans_transaction_type_id = trans_type_id;
            th.sales_date = sales_date;
            th.date_created = date_created;
            th.mf_document_type_id = doctype_id;
            th.crm_branch_id = branch_id;
            th.crm_from_branch_id = from_branch_id;
            th.crm_to_branch_id = to_branch_id;
            th.crm_customer_id = customer_id;
            th.crm_user_id = user_id;
            th.crm_admin_id = admin_id;
            th.inv_discount_id = discount_id;
            th.mf_is_void_status_id = is_void_status_id;
            th.mf_open_status_id = open_status_id;
            th.discount_amount = discount_amount;
            th.comment = comment;
            db.trans_transaction_header.Add(th);

            //Detail Header ID
            //cls_header_id = long.Parse(th.id.ToString());
            //HttpSessionStateBase["header_id"] = cls_header_id;
            
            //Save Header
            db.SaveChanges();
            cls_header_id = long.Parse(th.id.ToString());
        }

        public static void add_transaction_detail(int? item_id, int? uom_id, int? tax_id, int? discount_id, int? user_id, int? admin_id,
                        int? status_id, decimal? qty, decimal? inv_qty, decimal? tax_amount, decimal? price_wo_tax, decimal? price_w_tax,
                        decimal? discount_rate, decimal? discount_amount, decimal? line_discount_applied, decimal? sub_total, decimal? extended_total,
                        string upc, string sku)
        {
            trans_transaction_detail td = new trans_transaction_detail();
                td.trans_transaction_header_id = cls_header_id;
                td.inv_item_id = item_id;
                td.inv_uom_id = uom_id;
                td.mf_tax_id = tax_id;
                td.inv_discount_id = discount_id;
                td.crm_user_id = user_id;
                td.crm_admin_id = admin_id;
                td.mf_status_id = status_id;
                td.qty = qty;
                td.inv_qty = inv_qty;
                td.tax_amount = tax_amount;
                td.price_wo_tax = price_wo_tax;
                td.price_w_tax = price_w_tax;
                td.discount_rate = discount_rate;
                td.discount_rate = discount_amount;
                td.line_discount_amount_applied = line_discount_applied;
                td.sub_total = sub_total;
                td.extended_total = extended_total;
                td.upc = upc;
                td.sku = sku;
            db.trans_transaction_detail.Add(td);
            db.SaveChanges();
        }



        public static decimal get_price(int item_id, int uom_id)
        {            
            try
            {
                inv_pricebook_detail pbd = db.inv_pricebook_detail.First(x => x.item_id == item_id && x.uom_id == uom_id);
                return decimal.Parse(pbd.price_vat_in.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public static decimal get_grand_total(Int64 id)
        {
            decimal total_amt = 0;
            decimal total_discount = 0;
            decimal grand_total = 0;
            try
            {
                var gt = from data in db.trans_transaction_detail
                         where data.trans_transaction_header_id == id
                         select data;
                total_amt = decimal.Parse(gt.Sum(x => x.extended_total).ToString());
                total_discount = decimal.Parse(gt.Sum(x => x.discount_amount).ToString());
                grand_total = total_amt - total_discount;
                return grand_total;
            }
            catch
            {
                return 0;
            }

        }
    }
}