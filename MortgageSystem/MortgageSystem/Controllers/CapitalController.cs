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
    public class CapitalController : Controller
    {
        private mortgageEntities db = new mortgageEntities();
        // GET: Capital
        public async Task<ActionResult> Index()
        {
            var data = from x in db.trans_payment_collection
                       where x.trans_transaction_header.mf_document_type_id == 9
                       select x;
            return View(await data.ToListAsync());
        }
        // GET: Capital/Create
        public ActionResult Create()
        {
            ViewBag.crm_branch_id = new SelectList(db.crm_branch, "id", "description");
            ViewBag.mf_is_void_status_id = new SelectList(db.mf_status, "id", "description");
            return View();
        }

        // POST: Capital/Create   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( string crm_branch_id, string mf_is_void_status_id,string amount, string comment)
        {
            trans_transaction_header th = new trans_transaction_header();
            th.trans_transaction_type_id = 1; //regular
            th.date_created = DateTime.Now;
            th.mf_document_type_id = 9; //Capital
            th.crm_branch_id = int.Parse(crm_branch_id);
            th.crm_user_id = int.Parse(Session["user_id"].ToString()); //temporary
            th.mf_is_void_status_id = int.Parse(mf_is_void_status_id); //Void Status

            //Save Header
            db.trans_transaction_header.Add(th);

            //Save payment_collection
            int payment_type_id = 1;//cash
            payment_collection(th.id, payment_type_id, int.Parse(Session["user_id"].ToString()), int.Parse(crm_branch_id), 5,decimal.Parse(amount),comment);

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public void payment_collection(Int64 trans_header_id, int payment_type_id, int user_id, int branch_id, int is_void_status_id,decimal amount, string comment)
        {
            trans_payment_collection pc = new trans_payment_collection();
            pc.trans_transaction_header_id = trans_header_id;
            pc.mf_payment_type_id = payment_type_id;
            pc.crm_user_id = user_id;
            pc.mf_status_id = is_void_status_id;
            pc.amount = amount;
            pc.total_interest_amount = 0;
            pc.open_balance_amount = amount;
            pc.payment_date = DateTime.Now;
            pc.sales_date = DateTime.Now;
            pc.discount_amount = 0;
            pc.penalty_amount = 0;
            pc.comment = comment;
            db.trans_payment_collection.Add(pc);
            db.SaveChanges();

        }



    }
}