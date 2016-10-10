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
    public class dependentController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: dependent
        public async Task<ActionResult> Index()
        {
            var crm_dependent = db.crm_dependent.Include(c => c.crm_customer).Include(c => c.crm_address);
            return View(await crm_dependent.ToListAsync());
        }

        // GET: dependent/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_dependent crm_dependent = await db.crm_dependent.FindAsync(id);
            if (crm_dependent == null)
            {
                return HttpNotFound();
            }
            return View(crm_dependent);
        }

        // GET: dependent/Create
        public ActionResult Create()
        {
            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name");
            ViewBag.crm_address_id = new SelectList(db.crm_address, "id", "description");
            return View();
        }

        // POST: dependent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,crm_customer_id,crm_address_id,name")] crm_dependent crm_dependent)
        {
            if (ModelState.IsValid)
            {
                db.crm_dependent.Add(crm_dependent);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name", crm_dependent.crm_customer_id);
            ViewBag.crm_address_id = new SelectList(db.crm_address, "id", "description", crm_dependent.crm_address_id);
            return View(crm_dependent);
        }

        // GET: dependent/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_dependent crm_dependent = await db.crm_dependent.FindAsync(id);
            if (crm_dependent == null)
            {
                return HttpNotFound();
            }
            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name", crm_dependent.crm_customer_id);
            ViewBag.crm_address_id = new SelectList(db.crm_address, "id", "description", crm_dependent.crm_address_id);
            return View(crm_dependent);
        }

        // POST: dependent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,crm_customer_id,crm_address_id,name")] crm_dependent crm_dependent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crm_dependent).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name", crm_dependent.crm_customer_id);
            ViewBag.crm_address_id = new SelectList(db.crm_address, "id", "description", crm_dependent.crm_address_id);
            return View(crm_dependent);
        }

        // GET: dependent/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_dependent crm_dependent = await db.crm_dependent.FindAsync(id);
            if (crm_dependent == null)
            {
                return HttpNotFound();
            }
            return View(crm_dependent);
        }

        // POST: dependent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            crm_dependent crm_dependent = await db.crm_dependent.FindAsync(id);
            db.crm_dependent.Remove(crm_dependent);
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
