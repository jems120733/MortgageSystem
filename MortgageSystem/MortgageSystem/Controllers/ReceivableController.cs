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

namespace MortgageSystem.Controllers
{

    public class ReceivableController : Controller
    {
        mortgageEntities db = new mortgageEntities();
        // GET: Receivable
        [HttpGet]
        public ActionResult Index()
        {
            payment_collection_data.Clear();
            var data = from c in db.trans_payment_collection.AsNoTracking()
                       where c.trans_transaction_header.mf_document_type_id == 1
                       || c.trans_transaction_header.mf_document_type_id == 8

                       select new
                       {
                                transaction_id = c.trans_transaction_header.mf_document_type.id,
                                transaction_description = c.trans_transaction_header.mf_document_type.description,
                                customer_id = c.trans_transaction_header.crm_customer_id,
                                first_name = c.trans_transaction_header.crm_customer.first_name,
                                middle_name = c.trans_transaction_header.crm_customer.middle_name,
                                last_name = c.trans_transaction_header.crm_customer.last_name,
                                amount = c.amount,
                                open_balance = c.open_balance_amount,
                                discount = c.discount_amount
                            } into x
                            group x by new
                            {
                                x.transaction_id,
                                x.transaction_description,
                                x.customer_id,
                                x.first_name,
                                x.middle_name,
                                x.last_name
                            } into g
                            select new
                            {
                                g.Key.transaction_id,
                                g.Key.transaction_description,
                                g.Key.customer_id,
                                g.Key.first_name,
                                g.Key.middle_name,
                                g.Key.last_name,                               
                                open_balance = g.Sum(c => c.open_balance) * -1
                            };

            foreach (var item in data)
            {
                payment_collection_data.Add(new cls_payment_collection
                {
                    transaction_id = item.transaction_id,
                    transaction_description = item.transaction_description,
                    customer_id = item.customer_id,
                    first_name = item.first_name,
                    middle_name = item.middle_name,
                    last_name = item.last_name,
                    open_balance = item.open_balance

                });
            }

            return View(payment_collection_data);
        }

        List<cls_payment_collection> payment_collection_data = new List<cls_payment_collection>();
        public class cls_payment_collection
        {
            public Int64 transaction_id { set; get; }
            public string transaction_description { set; get; }
            public Int64? customer_id { set; get; }
            public string first_name { set; get; }
            public string middle_name { set; get; }
            public string last_name { set; get; }
            public decimal amount { set; get; }
            public decimal discount_amount { set; get; }
            public decimal open_balance { set; get; }
        }
        
    }
}