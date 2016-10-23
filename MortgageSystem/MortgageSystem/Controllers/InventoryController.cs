using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MortgageSystem.Models;

namespace MortgageSystem.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        mortgageEntities db = new mortgageEntities();
        //public int item_id { set; get; }
        //public string item_description { set; get; }
        //public decimal inv_qty { set; get; }
        public ActionResult Index()
        {
            inventory_data.Clear();
            var data = from c in db.trans_transaction_detail.AsNoTracking()
                       select new
                       {
                           item_id = c.inv_item_id,
                           item_description = c.inv_item.full_description,
                           inv_qty = c.inv_qty
                       } into x
                       group x by new
                       {
                           x.item_id,
                           x.item_description
                       } into g
                       select new
                       {
                           g.Key.item_id,
                           g.Key.item_description,
                           inv_qty = g.Sum(c => c.inv_qty)
                       };

            foreach (var item in data)
            {
                inventory_data.Add(new cls_inventory
                {
                    item_id = item.item_id,
                    item_description = item.item_description,
                    inv_qty = item.inv_qty
                });
            }
            return View(inventory_data);
        }

        public List<cls_inventory> inventory_data = new List<cls_inventory>();
        public class cls_inventory
        {
            public int? item_id { set; get; }
            public string item_description { set; get; }
            public decimal? inv_qty { set; get; }
        }
    }
}