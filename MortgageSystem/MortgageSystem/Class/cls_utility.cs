using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MortgageSystem.Models;

namespace MortgageSystem.Class
{    
    public  class cls_utility
    {
        private mortgageEntities db = new mortgageEntities();
        public decimal price(int item_id)
        {                
            try
            {
                inv_pricebook_detail pbd = db.inv_pricebook_detail.First(x => x.item_id == item_id);
                return decimal.Parse(pbd.price_vat_in.ToString());
            }
            catch
            {
                return 0;
            }

        }       
    }
}