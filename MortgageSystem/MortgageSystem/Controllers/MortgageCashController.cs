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

namespace MortgageSystem.Views
{
    public class MortgageCashController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: MortgageCash
        public async Task<ActionResult> Index()
        {
            Session["user_id"] = 1;
            var trans_transaction_header = db.trans_transaction_header.Include(t => t.crm_branch).Include(t => t.crm_branch1).Include(t => t.crm_branch2).Include(t => t.crm_customer).Include(t => t.crm_user).Include(t => t.crm_user1).Include(t => t.inv_discount).Include(t => t.mf_document_type).Include(t => t.mf_status).Include(t => t.mf_status1).Include(t => t.trans_transaction_type);
            return View(await trans_transaction_header.ToListAsync());
        }

        // GET: MortgageCash
        public long local_header_id;
        public ActionResult Payment_form(long id)
        {

            //var trans_transaction_header = db.trans_transaction_header.Include(t => t.crm_branch).Include(t => t.crm_branch1).Include(t => t.crm_branch2).Include(t => t.crm_customer).Include(t => t.crm_user).Include(t => t.crm_user1).Include(t => t.inv_discount).Include(t => t.mf_document_type).Include(t => t.mf_status).Include(t => t.mf_status1).Include(t => t.trans_transaction_type);
            //return View(await trans_transaction_header.ToListAsync());
            local_header_id = id;

            return View();
        }

        // GET: MortgageCash/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trans_transaction_header trans_transaction_header = await db.trans_transaction_header.FindAsync(id);
            if (trans_transaction_header == null)
            {
                return HttpNotFound();
            }

            crm_mortgage_daily_payables mdp = db.crm_mortgage_daily_payables.First(x => x.trans_transaction_header_id == id);
            ViewBag.enc_period = mdp.period;
            ViewBag.enc_amt_borrowed = mdp.amount_borrowed.ToString("#,##.00");
            ViewBag.enc_total_interest = mdp.total_interest.ToString("#,##.00");
            ViewBag.rate = ((decimal.Parse(mdp.total_interest.ToString()) / decimal.Parse(mdp.amount_borrowed.ToString())) * 100).ToString("#,##.00");
            ViewBag.enc_daily_amt_paypable = mdp.daily_amount_payables.ToString("#,##.00");
            ViewBag.enc_date_started = mdp.date_started.ToString("MMMM dd, yyyy");
            ViewBag.enc_date_ended = mdp.date_ended.ToString("MMMM dd, yyyy");
            ViewBag.enc_total_amount = (decimal.Parse(mdp.amount_borrowed.ToString()) + decimal.Parse(mdp.total_interest.ToString())).ToString("#,##.00");
            ViewBag.enc_daily_interest = (decimal.Parse(mdp.total_interest.ToString()) / decimal.Parse(mdp.period.ToString())).ToString("#,##.00"); 
            ViewBag.crm_branch_id = trans_transaction_header.crm_branch.description;

            return View(trans_transaction_header);
        }

        // GET: MortgageCash/Create
        public ActionResult Create()
        {
            ViewBag.crm_branch_id = new SelectList(db.crm_branch, "id", "description");            
            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name");
            ViewBag.period = new SelectList(db.mf_payment_terms, "period", "period");
            ViewBag.rate = new SelectList(db.mf_payment_terms, "rate", "rate");
            ViewBag.mf_open_status_id = new SelectList(db.mf_status, "id", "description");            
            return View();
        }

        // POST: MortgageCash/Create   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string from_date, string to_date, string crm_branch_id, string crm_customer_id, string mf_open_status_id,
                                                string period,string rate, string amount, string daily_payable, string total_amount,string daily_interest,
                                                string total_interest, string comment)
        {

            trans_transaction_header th = new trans_transaction_header();
            th.trans_transaction_type_id = 1; //regular
            th.sales_date = DateTime.Parse(from_date);
            th.date_created = DateTime.Now;
            th.mf_document_type_id = 1; //Mortgage Cash
            th.crm_branch_id = int.Parse(crm_branch_id);
            th.crm_customer_id = Int64.Parse(crm_customer_id);
            th.crm_user_id = int.Parse(Session["user_id"].ToString()); //temporary
            th.mf_is_void_status_id = 5; //Not Voided
            th.mf_open_status_id = int.Parse(mf_open_status_id); //Grant Status
            th.comment = comment;

            //Save Header
            db.trans_transaction_header.Add(th);

            //mortgage_daily_payables
            mortgage_daily_payables(th.id, Int64.Parse(crm_customer_id), int.Parse(Session["user_id"].ToString()), int.Parse(period), 
                DateTime.Now, decimal.Parse(amount), decimal.Parse(total_interest),decimal.Parse(daily_payable), DateTime.Parse(from_date), DateTime.Parse(to_date));

            //payment_collection
            int payment_type_id = 1;//cash
            payment_collection(th.id, payment_type_id, int.Parse(Session["user_id"].ToString()), int.Parse(crm_branch_id), 5, decimal.Parse(amount),
                decimal.Parse(amount), DateTime.Now,DateTime.Parse(from_date), comment, 0);

            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public void update_payment_collection(Int64 trans_header_id, int payment_type_id, int user_id, int branch_id, int is_void_status_id,
            decimal amount, decimal open_balance_amount, DateTime payment_date, DateTime sales_date, string comment, decimal discount)
        {            
            trans_payment_collection pc = db.trans_payment_collection.First(x => x.trans_transaction_header_id == trans_header_id);
            pc.trans_transaction_header_id = trans_header_id;
            pc.mf_payment_type_id = payment_type_id;
            pc.crm_user_id = user_id;
            pc.crm_branch_id = branch_id;
            pc.mf_status_id = is_void_status_id;
            pc.amount = amount;
            pc.open_balance_amount = amount * (-1);
            pc.payment_date = payment_date;
            pc.sales_date = sales_date;
            pc.comment = comment;
            pc.discount_amount = discount;

            db.SaveChanges();

        }
        public void payment_collection(Int64 trans_header_id,int payment_type_id, int user_id, int branch_id,int is_void_status_id,
            decimal amount,decimal open_balance_amount, DateTime payment_date, DateTime sales_date,string comment, decimal discount)
        {
            trans_payment_collection pc = new trans_payment_collection();
            pc.trans_transaction_header_id = trans_header_id;
            pc.mf_payment_type_id = payment_type_id;
            pc.crm_user_id = user_id;            
            pc.crm_branch_id = branch_id;
            pc.mf_status_id = is_void_status_id;
            pc.amount = amount;
            pc.open_balance_amount = amount * (-1);
            pc.payment_date = payment_date;
            pc.sales_date = sales_date;
            pc.comment = comment;
            pc.discount_amount = discount;

            db.trans_payment_collection.Add(pc);
            db.SaveChanges();

        }


        public void mortgage_daily_payables(Int64 trans_header_id, Int64 customer_id, int user_id, int period, DateTime trans_date, 
                                    decimal amount_borrowed, decimal total_interest, decimal daily_amount_payables, DateTime date_started, DateTime date_end)
        {
            crm_mortgage_daily_payables mdp = new crm_mortgage_daily_payables();
            mdp.crm_customer_id = customer_id;
            mdp.user_id = user_id;
            mdp.period = period;
            mdp.transaction_date = trans_date;
            mdp.amount_borrowed = amount_borrowed;
            mdp.total_interest = total_interest;
            mdp.daily_amount_payables = daily_amount_payables;
            mdp.date_started = date_started;
            mdp.date_ended = date_end;

            db.crm_mortgage_daily_payables.Add(mdp);
            db.SaveChanges();
            
        }

        
        public void update_mortgage_daily_payables(Int64 trans_header_id, Int64 customer_id, int user_id, int period, DateTime trans_date,
                                    decimal amount_borrowed, decimal total_interest, decimal daily_amount_payables, DateTime date_started, DateTime date_end)
        {
            crm_mortgage_daily_payables mdp = db.crm_mortgage_daily_payables.First(x => x.trans_transaction_header_id == trans_header_id);
            mdp.crm_customer_id = customer_id;
            mdp.user_id = user_id;
            mdp.period = period;
            mdp.transaction_date = trans_date;
            mdp.amount_borrowed = amount_borrowed;
            mdp.total_interest = total_interest;
            mdp.daily_amount_payables = daily_amount_payables;
            mdp.date_started = date_started;
            mdp.date_ended = date_end;
            db.SaveChanges();

        }

        // GET: MortgageCash/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trans_transaction_header trans_transaction_header = await db.trans_transaction_header.FindAsync(id);
            

            if (trans_transaction_header == null)
            {
                return HttpNotFound();
            }
           
            crm_mortgage_daily_payables mdp = db.crm_mortgage_daily_payables.First(x => x.trans_transaction_header_id == id);
            ViewBag.enc_period = mdp.period;
            ViewBag.enc_amt_borrowed = mdp.amount_borrowed.ToString("###.00");
            ViewBag.enc_total_interest = mdp.total_interest.ToString("###.00");
            ViewBag.enc_daily_amt_paypable = mdp.daily_amount_payables.ToString("###.00");
            ViewBag.enc_date_started = mdp.date_started.ToString("MM/dd/yyyy");
            ViewBag.enc_date_ended = mdp.date_ended.ToString("MM/dd/yyyy");
            ViewBag.enc_total_amount = (decimal.Parse(mdp.amount_borrowed.ToString()) + decimal.Parse(mdp.total_interest.ToString())).ToString("###.00"); ;
            ViewBag.enc_daily_interest = (decimal.Parse(mdp.total_interest.ToString()) / decimal.Parse(mdp.period.ToString())).ToString("###.00"); ;

            ViewBag.crm_branch_id = new SelectList(db.crm_branch, "id", "description", trans_transaction_header.crm_branch_id);
            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name", trans_transaction_header.crm_customer_id);
            ViewBag.period = new SelectList(db.mf_payment_terms, "period", "period");
            ViewBag.rate = new SelectList(db.mf_payment_terms, "rate", "rate");
            ViewBag.mf_open_status_id = new SelectList(db.mf_status, "id", "description", trans_transaction_header.mf_open_status_id);
            ViewBag.mf_is_void_status_id = new SelectList(db.mf_status, "id", "description", trans_transaction_header.mf_is_void_status_id);
            
            return View(trans_transaction_header);
        }


        // POST: MortgageCash/Edit   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, string from_date, string to_date, string crm_branch_id, string crm_customer_id, string mf_open_status_id,
            string mf_is_void_status_id, string period, string rate, string amount, string daily_payable, string total_amount, string daily_interest,
                                                string total_interest, string comment)
        {
            Int64 header_id = Int64.Parse(id.ToString());
            trans_transaction_header th = db.trans_transaction_header.First(i => i.id == header_id);
            {                
                th.sales_date = DateTime.Parse(from_date);
                th.date_created = DateTime.Now;
                th.crm_branch_id = int.Parse(crm_branch_id);
                th.crm_customer_id = Int64.Parse(crm_customer_id);
                th.crm_user_id = int.Parse(Session["user_id"].ToString()); //temporary
                th.mf_is_void_status_id = int.Parse(mf_is_void_status_id);
                th.mf_open_status_id = int.Parse(mf_open_status_id); //Grant Status
                th.comment = comment;
            };            
            await db.SaveChangesAsync();

            //Update Mortage payables
            update_mortgage_daily_payables(header_id, Int64.Parse(crm_customer_id), int.Parse(Session["user_id"].ToString()), int.Parse(period), DateTime.Now, decimal.Parse(amount), decimal.Parse(total_interest),
                                decimal.Parse(daily_payable), DateTime.Parse(from_date), DateTime.Parse(to_date));

            //update payment_collection
            int payment_type_id = 1;//cash
            update_payment_collection(th.id, payment_type_id, int.Parse(Session["user_id"].ToString()), int.Parse(crm_branch_id), 5, decimal.Parse(amount),
                decimal.Parse(amount), DateTime.Now,DateTime.Parse(from_date), comment, 0);



            return RedirectToAction("Index");
        }

        

        // GET: MortgageCash/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trans_transaction_header trans_transaction_header = await db.trans_transaction_header.FindAsync(id);
            if (trans_transaction_header == null)
            {
                return HttpNotFound();
            }
            return View(trans_transaction_header);
        }

        // POST: MortgageCash/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            delete_mortgage_daily_payable(id);
            delete_payment_collection(id);
            trans_transaction_header trans_transaction_header = await db.trans_transaction_header.FindAsync(id);
            db.trans_transaction_header.Remove(trans_transaction_header);
            await db.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }

        public void delete_mortgage_daily_payable(long header_id)
        {
            crm_mortgage_daily_payables mdp = db.crm_mortgage_daily_payables.First(x => x.trans_transaction_header_id == header_id);
            db.crm_mortgage_daily_payables.Remove(mdp);
            db.SaveChanges();
            
        }

        public void delete_payment_collection(long header_id)
        {
            trans_payment_collection pc = db.trans_payment_collection.First(x => x.trans_transaction_header_id == header_id);
            db.trans_payment_collection.Remove(pc);
            db.SaveChanges();

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
