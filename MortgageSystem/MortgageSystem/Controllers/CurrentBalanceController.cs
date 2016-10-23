using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MortgageSystem.Models;
namespace MortgageSystem.Controllers
{
    public class CurrentBalanceController : Controller
    {
        mortgageEntities db = new mortgageEntities();

        // GET: CurrentBalance
        public ActionResult Index()
        {
            decimal current_balance = 0;
            try
            {
                var data = db.trans_payment_collection.AsNoTracking();
                current_balance = decimal.Parse(data.Sum(x => x.open_balance_amount).ToString()) + decimal.Parse(data.Sum(x => x.total_interest_amount).ToString());
            }
            catch
            {}
            ViewBag.current_balance = current_balance;
            return View(payment_collection_data);

        }

        List<cls_payment_collection> payment_collection_data = new List<cls_payment_collection>();
        public class cls_payment_collection
        {            
            public decimal open_balance { set; get; }
        }
    }
}