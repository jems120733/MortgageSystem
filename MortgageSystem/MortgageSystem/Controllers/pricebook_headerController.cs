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
    public class pricebook_headerController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: pricebook_header
        public async Task<ActionResult> Index()
        {
            var inv_pricebook_header = db.inv_pricebook_header.Include(i => i.crm_branch).Include(i => i.crm_customer).Include(i => i.inv_pricebook_type).Include(i => i.mf_status);
            return View(await inv_pricebook_header.ToListAsync());
        }

        // GET: pricebook_header/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv_pricebook_header inv_pricebook_header = await db.inv_pricebook_header.FindAsync(id);
            if (inv_pricebook_header == null)
            {
                return HttpNotFound();
            }
            return View(inv_pricebook_header);
        }

        // GET: pricebook_header/Create
        public ActionResult Create()
        {
            ViewBag.branch_id = new SelectList(db.crm_branch, "id", "description");
            ViewBag.customer_id = new SelectList(db.crm_customer, "id", "first_name");
            ViewBag.pricebook_type_id = new SelectList(db.inv_pricebook_type, "id", "description");
            ViewBag.status_id = new SelectList(db.mf_status, "id", "description");
            return View();
        }

        // POST: pricebook_header/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,description,branch_id,customer_id,pricebook_type_id,status_id")] inv_pricebook_header inv_pricebook_header)
        {
            if (ModelState.IsValid)
            {
                db.inv_pricebook_header.Add(inv_pricebook_header);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.branch_id = new SelectList(db.crm_branch, "id", "description", inv_pricebook_header.branch_id);
            ViewBag.customer_id = new SelectList(db.crm_customer, "id", "first_name", inv_pricebook_header.customer_id);
            ViewBag.pricebook_type_id = new SelectList(db.inv_pricebook_type, "id", "description", inv_pricebook_header.pricebook_type_id);
            ViewBag.status_id = new SelectList(db.mf_status, "id", "description", inv_pricebook_header.status_id);
            return View(inv_pricebook_header);
        }

        // GET: pricebook_header/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv_pricebook_header inv_pricebook_header = await db.inv_pricebook_header.FindAsync(id);
            if (inv_pricebook_header == null)
            {
                return HttpNotFound();
            }
            ViewBag.branch_id = new SelectList(db.crm_branch, "id", "description", inv_pricebook_header.branch_id);
            ViewBag.customer_id = new SelectList(db.crm_customer, "id", "first_name", inv_pricebook_header.customer_id);
            ViewBag.pricebook_type_id = new SelectList(db.inv_pricebook_type, "id", "description", inv_pricebook_header.pricebook_type_id);
            ViewBag.status_id = new SelectList(db.mf_status, "id", "description", inv_pricebook_header.status_id);
            return View(inv_pricebook_header);
        }

        // POST: pricebook_header/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,description,branch_id,customer_id,pricebook_type_id,status_id")] inv_pricebook_header inv_pricebook_header)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inv_pricebook_header).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.branch_id = new SelectList(db.crm_branch, "id", "description", inv_pricebook_header.branch_id);
            ViewBag.customer_id = new SelectList(db.crm_customer, "id", "first_name", inv_pricebook_header.customer_id);
            ViewBag.pricebook_type_id = new SelectList(db.inv_pricebook_type, "id", "description", inv_pricebook_header.pricebook_type_id);
            ViewBag.status_id = new SelectList(db.mf_status, "id", "description", inv_pricebook_header.status_id);
            return View(inv_pricebook_header);
        }

        // GET: pricebook_header/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv_pricebook_header inv_pricebook_header = await db.inv_pricebook_header.FindAsync(id);
            if (inv_pricebook_header == null)
            {
                return HttpNotFound();
            }
            return View(inv_pricebook_header);
        }

        // POST: pricebook_header/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            inv_pricebook_header inv_pricebook_header = await db.inv_pricebook_header.FindAsync(id);
            db.inv_pricebook_header.Remove(inv_pricebook_header);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
