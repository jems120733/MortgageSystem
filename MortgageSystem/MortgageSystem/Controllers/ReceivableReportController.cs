using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MortgageSystem.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace MortgageSystem.Controllers
{
    [Authorize]
    public class ReceivableReportController : Controller
    {
        // GET: ReceivableReport
        mortgageEntities db = new mortgageEntities();

        [HttpGet]
        public JsonResult API_()
        {
            var receivable = (from data in db.trans_payment_collection
                              where data.trans_transaction_header.mf_document_type_id == 1
                              || data.trans_transaction_header.mf_document_type_id == 8
                              select new
                              {
                                  data.trans_transaction_header.crm_customer_id,
                                  data.trans_transaction_header.crm_customer.first_name,
                                  data.trans_transaction_header.crm_customer.middle_name,
                                  data.trans_transaction_header.crm_customer.last_name,
                                  data.open_balance_amount,
                                  discount_amount = data.discount_amount
                              }
                             into x
                              group x by new
                              {
                                  x.crm_customer_id,
                                  x.first_name,
                                  x.middle_name,
                                  x.last_name

                              } into g

                              select new
                              {
                                  g.Key.crm_customer_id,
                                  g.Key.first_name,
                                  g.Key.middle_name,
                                  g.Key.last_name,
                                  ob_amount = (g.Sum(data => data.open_balance_amount) - g.Sum(data => data.discount_amount))
                              }).ToList();
            return Json(receivable, JsonRequestBehavior.AllowGet);
        }




        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var receivable = (from data in db.trans_payment_collection
                              where data.trans_transaction_header.mf_document_type_id == 1
                              || data.trans_transaction_header.mf_document_type_id == 8
                              select new
                              {
                                  data.trans_transaction_header.crm_customer_id,
                                  data.trans_transaction_header.crm_customer.first_name,
                                  data.trans_transaction_header.crm_customer.middle_name,
                                  data.trans_transaction_header.crm_customer.last_name,
                                  data.open_balance_amount,
                                  discount_amount = data.discount_amount
                              }
                             into x
                              group x by new
                              {
                                  x.crm_customer_id,
                                  x.first_name,
                                  x.middle_name,
                                  x.last_name

                              } into g

                              select new
                              {
                                  g.Key.crm_customer_id,
                                  g.Key.first_name,
                                  g.Key.middle_name,
                                  g.Key.last_name,
                                  ob_amount = (g.Sum(data => data.open_balance_amount) - g.Sum(data => data.discount_amount))
                              }).ToList();


            //var javaScriptSerializer = new JavaScriptSerializer();
            //string jsonString = javaScriptSerializer.Serialize(receivable);
            //ViewBag.receivable = receivable;// jsonString;
            return View(receivable);
            //return View(new { receivable = jsonString });

        }
    }
}