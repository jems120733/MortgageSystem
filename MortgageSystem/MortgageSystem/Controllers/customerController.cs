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
    public class customerController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: customer
        public async Task<ActionResult> Index()
        {
            var crm_customer = db.crm_customer.Include(c => c.crm_civil_status);
            return View(await crm_customer.ToListAsync());
        }

        // GET: customer/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_customer crm_customer = await db.crm_customer.FindAsync(id);
            if (crm_customer == null)
            {
                return HttpNotFound();
            }
            return View(crm_customer);
        }

        // GET: customer/Create
        public ActionResult Create()
        {
            ViewBag.crm_civil_status_id = new SelectList(db.crm_civil_status, "id", "description");
            return View();
        }

        // POST: customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,first_name,middle_name,last_name,birthday,birth_place,crm_civil_status_id")] crm_customer crm_customer)
        {
            if (ModelState.IsValid)
            {
                db.crm_customer.Add(crm_customer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.crm_civil_status_id = new SelectList(db.crm_civil_status, "id", "description", crm_customer.crm_civil_status_id);
            return View(crm_customer);
        }

        // GET: customer/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_customer crm_customer = await db.crm_customer.FindAsync(id);
            if (crm_customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.crm_civil_status_id = new SelectList(db.crm_civil_status, "id", "description", crm_customer.crm_civil_status_id);
            ViewBag.birthday = crm_customer.birthday.ToShortDateString();
            return View(crm_customer);
        }

        // POST: customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,first_name,middle_name,last_name,birthday,birth_place,crm_civil_status_id")] crm_customer crm_customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crm_customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.crm_civil_status_id = new SelectList(db.crm_civil_status, "id", "description", crm_customer.crm_civil_status_id);
            return View(crm_customer);
        }

        // GET: customer/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_customer crm_customer = await db.crm_customer.FindAsync(id);
            if (crm_customer == null)
            {
                return HttpNotFound();
            }
            return View(crm_customer);
        }

        // POST: customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            crm_customer crm_customer = await db.crm_customer.FindAsync(id);
            db.crm_customer.Remove(crm_customer);
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
