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
    public class customer_contactController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: customer_contact
        public async Task<ActionResult> Index()
        {
            var crm_customer_contact = db.crm_customer_contact.Include(c => c.crm_customer);
            return View(await crm_customer_contact.ToListAsync());
        }

        // GET: customer_contact/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_customer_contact crm_customer_contact = await db.crm_customer_contact.FindAsync(id);
            if (crm_customer_contact == null)
            {
                return HttpNotFound();
            }
            return View(crm_customer_contact);
        }

        // GET: customer_contact/Create
        public ActionResult Create()
        {
            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name");
            return View();
        }

        // POST: customer_contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,crm_customer_id,description")] crm_customer_contact crm_customer_contact)
        {
            if (ModelState.IsValid)
            {
                db.crm_customer_contact.Add(crm_customer_contact);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name", crm_customer_contact.crm_customer_id);
            return View(crm_customer_contact);
        }

        // GET: customer_contact/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_customer_contact crm_customer_contact = await db.crm_customer_contact.FindAsync(id);
            if (crm_customer_contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name", crm_customer_contact.crm_customer_id);
            return View(crm_customer_contact);
        }

        // POST: customer_contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,crm_customer_id,description")] crm_customer_contact crm_customer_contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crm_customer_contact).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name", crm_customer_contact.crm_customer_id);
            return View(crm_customer_contact);
        }

        // GET: customer_contact/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_customer_contact crm_customer_contact = await db.crm_customer_contact.FindAsync(id);
            if (crm_customer_contact == null)
            {
                return HttpNotFound();
            }
            return View(crm_customer_contact);
        }

        // POST: customer_contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            crm_customer_contact crm_customer_contact = await db.crm_customer_contact.FindAsync(id);
            db.crm_customer_contact.Remove(crm_customer_contact);
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
