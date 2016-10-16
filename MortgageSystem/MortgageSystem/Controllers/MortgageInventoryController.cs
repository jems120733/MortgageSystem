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
    public class MortgageInventoryController : Controller
    {
        private mortgageEntities db = new mortgageEntities();
        // GET: MortgageInventory
        public async Task<ActionResult> Index()
        {
            var crm_mortgage_daily_payable = from data in db.crm_mortgage_daily_payables
                                             where data.trans_transaction_header.mf_document_type_id == 8
                                             select data;
            return View(await crm_mortgage_daily_payable.AsNoTracking().ToListAsync());
        }


        //GET Create Inventory
        public ActionResult Inventory()
        {
            ViewBag.inv_item_id = new SelectList(db.inv_item, "id", "short_description");
            ViewBag.inv_uom_id = new SelectList(db.inv_uom, "id", "description");
            return View();
        }

        //POST Create
        
        public void inventory_add(string id, string inv_item_id, string inv_uom_id, string qty, string price, string extended)
        {
            trans_transaction_detail td = new trans_transaction_detail();
                td.trans_transaction_header_id = Int64.Parse(id);
                td.inv_item_id = int.Parse(item_id);
                td.inv_uom_id = int.Parse(uom_id);
                td.mf_tax_id = 1;
                td.crm_user_id = int.Parse(Session["user_id"].ToString());
                td.mf_status_id = 5; //not voided#
                td.qty = decimal.Parse(qty);
                td.inv_qty = decimal.Parse(qty) * -1;
                td.tax_amount = 0;
                td.price_wo_tax = decimal.Parse(extended);
                td.discount_rate = 0;
                td.discount_amount = 0;
                td.line_discount_amount_applied = 0;
                td.sub_total = decimal.Parse(extended);
                td.extended_total = decimal.Parse(extended);
            db.trans_transaction_detail.Add(td);
            db.SaveChanges();
        }

        //GET List
        //public ActionResult Inventory_list()
        //{
        //    return View();
        //}

        //GET List
        public ActionResult Inventory_list(Int64? header_id)
        {
            var inventory = from data in db.trans_transaction_detail
                            where data.trans_transaction_header_id == header_id
                            select data;
            ViewBag.inventory = inventory.ToList();
            return View();
        }

        //Get Create
        public ActionResult Create()
        {
            ViewBag.crm_branch_id = new SelectList(db.crm_branch, "id", "description");
            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name");
            ViewBag.period = new SelectList(db.mf_payment_terms, "period", "period");
            ViewBag.rate = new SelectList(db.mf_payment_terms, "rate", "rate");
            ViewBag.mf_open_status_id = new SelectList(db.mf_status, "id", "description");
            ViewBag.id = Session["header_id"];
            return View();
        }


        public Int64 local_header_id;
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string from_date, string to_date, string crm_branch_id, string crm_customer_id, string mf_open_status_id,
                                                string period, string rate, string amount, string daily_payable, string total_amount, string daily_interest,
                                                string total_interest, string comment)
        {

            trans_transaction_header th = new trans_transaction_header();
            th.trans_transaction_type_id = 1; //regular
            th.sales_date = DateTime.Parse(from_date);
            th.date_created = DateTime.Now;
            th.mf_document_type_id = 8; //Mortgage Cash
            th.crm_branch_id = int.Parse(crm_branch_id);
            th.crm_customer_id = Int64.Parse(crm_customer_id);
            th.crm_user_id = int.Parse(Session["user_id"].ToString()); //temporary
            th.mf_is_void_status_id = 5; //Not Voided
            th.mf_open_status_id = int.Parse(mf_open_status_id); //Grant Status
            th.comment = comment;

            //Save Header
            db.trans_transaction_header.Add(th);

            //Save mortgage_daily_payables
            mortgage_daily_payables(th.id, Int64.Parse(crm_customer_id), int.Parse(Session["user_id"].ToString()), int.Parse(period),
                DateTime.Now, decimal.Parse(amount), decimal.Parse(total_interest), decimal.Parse(daily_payable), DateTime.Parse(from_date),
                DateTime.Parse(to_date), decimal.Parse(daily_interest));

            //header_id = 1054;//th.id;
            Session["header_id"] = th.id;
            

            await db.SaveChangesAsync();
            return RedirectToAction("Edit"+"/"+ Session["header_id"]);
        }


        // GET: MortgageInventory/Edit/5
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

            ViewBag.id = id;
            return View(trans_transaction_header);
        }

        public void mortgage_daily_payables(Int64 trans_header_id, Int64 customer_id, int user_id, int period, DateTime trans_date, decimal amount_borrowed,
            decimal total_interest, decimal daily_amount_payables, DateTime date_started, DateTime date_end, decimal daily_amount_interest)
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
                mdp.daily_amount_interest = daily_amount_interest;

                db.crm_mortgage_daily_payables.Add(mdp);
                db.SaveChanges();

            }
    }
}