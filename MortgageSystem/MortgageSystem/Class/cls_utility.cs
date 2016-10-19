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
    public  class cls_utility
    {
        static mortgageEntities db = new mortgageEntities();
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