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
    public class SalesInvoiceController : Controller
    {
        mortgageEntities db = new mortgageEntities();

        // GET: SalesInvoice
        public async  Task<ActionResult> Index()
        {
            var list = from data in db.trans_transaction_header
                       where data.mf_document_type_id == 4
                       select data;
            return View(await list.ToListAsync());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.crm_branch_id = new SelectList(db.crm_branch, "id", "description");
            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name");
            ViewBag.mf_open_status_id = new SelectList(db.mf_status, "id", "description");

            ViewBag.inv_item_id = new SelectList(db.inv_item, "id", "short_description");
            ViewBag.inv_uom_id = new SelectList(db.inv_uom, "id", "description");
            //ViewBag.id = Session["header_id"];
            return View();
        }

        /*
        public ActionResult Create(tbl_clone tblclone, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var description = form.GetValues("description").ToList();
                var number = form.GetValues("number").ToList();
                for (int x=0; x < int.Parse(description.Count().ToString()); x++)
                { 
                    tbl_clone data = new tbl_clone();
                    string desc = description[x].ToString();
                        data.number = int.Parse(number[x].ToString());
                        data.description = desc;
                        db.tbl_clone.Add(data);
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(tblclone);
        } 
        */


        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            //string sales_date, string comment, string crm_branch_id, string crm_customer_id

            //Start to be removed
            ViewBag.crm_branch_id = new SelectList(db.crm_branch, "id", "description");
            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name");
            ViewBag.mf_open_status_id = new SelectList(db.mf_status, "id", "description");

            ViewBag.inv_item_id = new SelectList(db.inv_item, "id", "short_description");
            ViewBag.inv_uom_id = new SelectList(db.inv_uom, "id", "description");
            //End to be removed

            DateTime sales_date = DateTime.Parse(form["sales_date"].ToString());
            int crm_branch_id = int.Parse(form["crm_branch_id"].ToString());
            long crm_customer_id = long.Parse(form["crm_customer_id"].ToString());
            string comment =form["comment"].ToString();

            cls_utility.add_transaction_header(1, sales_date, DateTime.Now, 4, crm_branch_id, null, null,
                        crm_customer_id, int.Parse(Session["user_id"].ToString()), null, null, 5, 3, 0, comment);

            var inv_item_id = form.GetValues("inv_item_id").ToList();
            var qty = form.GetValues("qty").ToList();
            var price = form.GetValues("price").ToList();
            var inv_uom_id = form.GetValues("inv_uom_id").ToList();
            var extended = form.GetValues("extended").ToList();

            for (int x = 0; x < int.Parse(inv_item_id.Count().ToString()); x++)
            {
                decimal inv_qty = decimal.Parse(qty[x].ToString()) * -1;
                cls_utility.add_transaction_detail(int.Parse(inv_item_id[x].ToString()), int.Parse(inv_uom_id[x].ToString()), null, null,
                    int.Parse(Session["user_id"].ToString()), null, 5, decimal.Parse(qty[x].ToString()), inv_qty, 0, decimal.Parse(price[x].ToString()),
                    decimal.Parse(price[x].ToString()), 0, 0, 0, decimal.Parse(price[x].ToString()), decimal.Parse(price[x].ToString()), null, null);
            }

            ViewBag.header_id = cls_utility.cls_header_id;
            return RedirectToAction("Index");
            //return View();
        }
    }
}