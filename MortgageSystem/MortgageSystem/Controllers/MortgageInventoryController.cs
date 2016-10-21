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


namespace MortgageSystem.Controllers
{
    public class MortgageInventoryController : Controller
    {
        private mortgageEntities db = new mortgageEntities();
      
        [HttpGet]
        public decimal get_price(string inv_item_id, string inv_uom_id)
        {
            int item_id_ = int.Parse(inv_item_id);
            int uom_id_ = int.Parse(inv_uom_id);

            decimal price = cls_utility.get_price(item_id_, uom_id_);
            return price;
        }

        public decimal get_grand_total(string id)
        {
            decimal gt = cls_utility.get_grand_total(Int64.Parse(id));
            return gt;
        }


        // GET: MortgageInventory
        public async Task<ActionResult> Index()
        {
            var crm_mortgage_daily_payable = from data in db.crm_mortgage_daily_payables
                                             where data.trans_transaction_header.mf_document_type_id == 8
                                             select data;
            return View(await crm_mortgage_daily_payable.AsNoTracking().ToListAsync());
        }

        //GET Create Inventory
        [HttpGet]
        public ActionResult Inventory()
        {
            ViewBag.inv_item_id = new SelectList(db.inv_item, "id", "short_description");
            ViewBag.inv_uom_id = new SelectList(db.inv_uom, "id", "description");
            ViewBag.id = Session["header_id"];
            return View();
        }

        //POST Create
        [HttpPost]
        public void Inventory(string id, string inv_item_id, string inv_uom_id, string qty, string price, string extended)
        {
            trans_transaction_detail td = new trans_transaction_detail();
                td.trans_transaction_header_id = Int64.Parse(id);
                td.inv_item_id = int.Parse(inv_item_id);
                td.inv_uom_id = int.Parse(inv_uom_id);
                td.mf_tax_id = 1;
                td.crm_user_id = int.Parse(Session["user_id"].ToString());
                td.mf_status_id = 5; //not voided#
                td.qty = decimal.Parse(qty);
                td.inv_qty = decimal.Parse(qty) * -1;
                td.tax_amount = 0;
                td.price_wo_tax = decimal.Parse(extended);
                td.price_w_tax = decimal.Parse(extended);
                td.discount_rate = 0;
                td.discount_amount = 0;
                td.line_discount_amount_applied = 0;
                td.sub_total = decimal.Parse(extended);
                td.extended_total = decimal.Parse(extended);

                db.trans_transaction_detail.Add(td);
                db.SaveChanges();
        }

        


        //GET List
        [HttpGet]
        public  ActionResult Inventory_list(string id)
        {
            Int64 header_id = Int64.Parse(id);
            var inventory = from data in db.trans_transaction_detail
                            where data.trans_transaction_header_id == header_id
                            select data;
            ViewBag.inventory = inventory.ToList();
            return View();
        }

        //Payment list  
        public ActionResult Payment_list(long id)
        {
            var list = from data in db.trans_payment_collection
                       where data.trans_transaction_header_id == id
                       select data;
            ViewBag.list = list.ToList();
            return View();
        }


        // GET: MortgageInvetory/Details/5
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
        public DateTime get_date_started(Int64 header_id)
        {
            try
            {
                crm_mortgage_daily_payables mdp = db.crm_mortgage_daily_payables.First(x => x.trans_transaction_header_id == header_id);
                return DateTime.Parse(mdp.date_started.ToString());
            }
            catch
            {
                return DateTime.Now;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Payment_form(string id, string sales_date, string mf_payment_type_id, string crm_collector_id, string amount, string comment, string last_payment_date)
        {
            DateTime last_payment = DateTime.Parse(last_payment_date);
            DateTime current_sales_date = DateTime.Parse(sales_date);

            //here
            DateTime date_started = get_date_started(Int64.Parse(id));
            Double date_diff = (current_sales_date - last_payment).TotalDays;
            Int64 header_id = Int64.Parse(id);

            crm_mortgage_daily_payables mdp = db.crm_mortgage_daily_payables.First(x => x.trans_transaction_header_id == header_id);
            decimal di = decimal.Parse(mdp.daily_amount_interest.ToString());

            date_diff = int.Parse(date_diff.ToString());

            //here
            if (date_diff == 1 || (date_started.Date == current_sales_date.Date))
            {
                trans_payment_collection pc = new trans_payment_collection();
                pc.trans_transaction_header_id = Int64.Parse(id);
                pc.mf_payment_type_id = int.Parse(mf_payment_type_id);
                pc.crm_user_id = int.Parse(Session["user_id"].ToString());
                pc.crm_collector_id = int.Parse(crm_collector_id);
                pc.amount = decimal.Parse(amount);
                pc.open_balance_amount = decimal.Parse(amount);
                pc.payment_date = DateTime.Now;
                pc.sales_date = DateTime.Parse(sales_date);
                pc.comment = comment;
                pc.mf_status_id = 5;
                pc.penalty_amount = 0;
                db.trans_payment_collection.Add(pc);
                //await db.SaveChangesAsync();
            }
            else
            {
                for (int x = 1; x <= date_diff; x++)
                {
                    decimal penalty_amount = 0;
                    if (last_payment.AddDays(x) != current_sales_date)
                    {
                        trans_payment_collection pc = new trans_payment_collection();
                        pc.trans_transaction_header_id = Int64.Parse(id);
                        pc.mf_payment_type_id = int.Parse(mf_payment_type_id);
                        pc.crm_user_id = int.Parse(Session["user_id"].ToString());
                        pc.crm_collector_id = int.Parse(crm_collector_id);
                        pc.amount = 0;
                        pc.open_balance_amount = di * -1;//decimal.Parse(amount) * -1 + di * (-1);
                        pc.payment_date = DateTime.Now;
                        pc.sales_date = last_payment.AddDays(x);
                        pc.comment = comment;
                        pc.mf_status_id = 5;
                        pc.penalty_amount = di * x;
                        penalty_amount = di * x;
                        db.trans_payment_collection.Add(pc);
                        //await db.SaveChangesAsync();
                    }
                    else
                    {
                        trans_payment_collection pc = new trans_payment_collection();
                        pc.trans_transaction_header_id = Int64.Parse(id);
                        pc.mf_payment_type_id = int.Parse(mf_payment_type_id);
                        pc.crm_user_id = int.Parse(Session["user_id"].ToString());
                        pc.crm_collector_id = int.Parse(crm_collector_id);
                        pc.amount = decimal.Parse(amount) + penalty_amount;
                        pc.open_balance_amount = decimal.Parse(amount);
                        pc.payment_date = DateTime.Now;
                        pc.sales_date = DateTime.Parse(sales_date);
                        pc.comment = comment;
                        pc.mf_status_id = 5;
                        pc.penalty_amount = 0;
                        db.trans_payment_collection.Add(pc);
                        //await db.SaveChangesAsync();
                    }
                }

            }
            await db.SaveChangesAsync();
            return RedirectToAction("Payment_list" + "/" + header_id);

            //return View();
        }
        // GET: Payment_form        
        DateTime global_date_started;
        public ActionResult Payment_form(Int64 id)
        {
            DateTime last_payment_date;
            try
            {
                trans_payment_collection pc = db.trans_payment_collection.OrderByDescending(x => x.id).First(x => x.trans_transaction_header_id == id && x.crm_collector_id > 0);
                last_payment_date = DateTime.Parse(pc.sales_date.ToString());
                ViewBag.last_payment_date = last_payment_date.ToShortDateString();
                ViewBag.message = last_payment_date.ToShortDateString();

            }
            catch
            {
                crm_mortgage_daily_payables mdp = db.crm_mortgage_daily_payables.First(x => x.trans_transaction_header_id == id);
                last_payment_date = DateTime.Parse((mdp.date_started.AddDays(-1).ToString()));
                ViewBag.last_payment_date = last_payment_date.ToShortDateString();
                ViewBag.message = last_payment_date.AddDays(1).ToShortDateString() + " - Started date";
            }



            trans_transaction_header th = db.trans_transaction_header.Find(id);
            ViewBag.mortgagor = th.crm_customer.last_name + ", " + th.crm_customer.first_name + " " + th.crm_customer.middle_name;
            ViewBag.mf_payment_type_id = new SelectList(db.mf_payment_type, "id", "description");
            ViewBag.crm_collector_id = new SelectList(db.crm_employee, "id", "last_name");
            ViewBag.header_id = id;

            //crm_mortgage_daily_payables mdp = db.crm_mortgage_daily_payables.First(x => x.trans_transaction_header_id == id);
            //mdp.daily_amount_payables
            //here
            ViewBag.amount = payable_amount(id, last_payment_date, DateTime.Now);
            return View();
        }

        public decimal payable_amount(Int64 header_id, DateTime last_payment_date, DateTime sales_date)
        {
            decimal total_payable = 0;

            DateTime last_payment = last_payment_date.Date;
            DateTime current_sales_date = sales_date.Date;
            decimal date_diff = decimal.Parse(((current_sales_date.Date - last_payment.Date).TotalDays).ToString());

            crm_mortgage_daily_payables mdp = db.crm_mortgage_daily_payables.First(x => x.trans_transaction_header_id == header_id);
            decimal di = decimal.Parse(mdp.daily_amount_interest.ToString());
            decimal daily_payable = decimal.Parse(mdp.daily_amount_payables.ToString());

            decimal total_di = decimal.Parse(di.ToString()) * (date_diff - 1);
            if (date_diff == 1)
            {
                total_payable = daily_payable;
            }
            else
            {
                for (int x = 1; x <= date_diff; x++)
                {

                    if (last_payment.AddDays(x) != current_sales_date)
                    {
                        total_payable += daily_payable;
                    }
                    else
                    {
                        total_payable += daily_payable;
                    }
                }

            }

            //if daily interest < 0, meaning, paid
            if (total_di < 0)
            { total_di = 0; };
            return decimal.Parse(total_payable.ToString()) + total_di;
        }

        //here
        //// GET: MortgageCash/Delete/5
        public void delete_inventory(int id)
       {
            try
            {
                trans_transaction_detail td = db.trans_transaction_detail.Find(id);// db.trans_transaction_detail.FindAsync(id);
                db.trans_transaction_detail.Remove(td);
                db.SaveChanges();
            }
            catch
            {
            }
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


        //public Int64 local_header_id;
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
            th.discount_amount = 0;
            th.comment = comment;

            //Save Header
            db.trans_transaction_header.Add(th);

            //Save mortgage_daily_payables
            mortgage_daily_payables(th.id, Int64.Parse(crm_customer_id), int.Parse(Session["user_id"].ToString()), int.Parse(period),
                DateTime.Now, decimal.Parse(amount), decimal.Parse(total_interest), decimal.Parse(daily_payable), DateTime.Parse(from_date),
                DateTime.Parse(to_date), decimal.Parse(daily_interest));

            //Save payment_collection
            int payment_type_id = 1;//cash
            payment_collection(th.id, payment_type_id, int.Parse(Session["user_id"].ToString()), int.Parse(crm_branch_id), 5, decimal.Parse(amount),
                decimal.Parse(amount), DateTime.Now, DateTime.Parse(from_date), comment, 0, decimal.Parse(total_interest));


            //header_id = 1054;//th.id;
            Session["header_id"] = th.id;
            await db.SaveChangesAsync();
            return RedirectToAction("Edit"+"/"+ Session["header_id"]);
        }

        public void payment_collection(Int64 trans_header_id, int payment_type_id, int user_id, int branch_id, int is_void_status_id,
            decimal amount, decimal open_balance_amount, DateTime payment_date, DateTime sales_date, string comment, decimal discount, decimal total_interest)
        {
            trans_payment_collection pc = new trans_payment_collection();
            pc.trans_transaction_header_id = trans_header_id;
            pc.mf_payment_type_id = payment_type_id;
            pc.crm_user_id = user_id;
            pc.crm_branch_id = branch_id;
            pc.mf_status_id = is_void_status_id;
            pc.amount = amount;
            pc.open_balance_amount = (total_interest + amount) * (-1);
            pc.payment_date = payment_date;
            pc.sales_date = sales_date;
            pc.comment = comment;
            pc.discount_amount = discount;
            pc.penalty_amount = 0;
            //pc.penalty_amount = (total_interest + total_interest )* (-1);
            db.trans_payment_collection.Add(pc);
            db.SaveChanges();

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

            Session["header_id"] = id;
            ViewBag.id = Session["header_id"];

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
                                decimal.Parse(daily_payable), DateTime.Parse(from_date), DateTime.Parse(to_date), decimal.Parse(daily_interest));

            //update payment_collection
            int payment_type_id = 1;//cash
            update_payment_collection(th.id, payment_type_id, int.Parse(Session["user_id"].ToString()), int.Parse(crm_branch_id), 5, decimal.Parse(amount),
                decimal.Parse(amount), DateTime.Now, DateTime.Parse(from_date), comment, 0, decimal.Parse(total_interest));



            return RedirectToAction("Index");
        }


        public void update_payment_collection(Int64 trans_header_id, int payment_type_id, int user_id, int branch_id, int is_void_status_id,
            decimal amount, decimal open_balance_amount, DateTime payment_date, DateTime sales_date, string comment, decimal discount, decimal total_interest)
        {
            trans_payment_collection pc = db.trans_payment_collection.First(x => x.trans_transaction_header_id == trans_header_id);
            pc.trans_transaction_header_id = trans_header_id;
            pc.mf_payment_type_id = payment_type_id;
            pc.crm_user_id = user_id;
            pc.crm_branch_id = branch_id;
            pc.mf_status_id = is_void_status_id;
            pc.amount = amount;
            pc.open_balance_amount = (total_interest + amount) * (-1);
            pc.payment_date = payment_date;
            pc.sales_date = sales_date;
            pc.comment = comment;
            pc.discount_amount = discount;

            db.SaveChanges();

        }

        public void update_mortgage_daily_payables(Int64 trans_header_id, Int64 customer_id, int user_id, int period, DateTime trans_date, decimal amount_borrowed,
                       decimal total_interest, decimal daily_amount_payables, DateTime date_started, DateTime date_end, decimal daily_amount_interest)
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
            mdp.daily_amount_interest = daily_amount_interest;

            db.SaveChanges();

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