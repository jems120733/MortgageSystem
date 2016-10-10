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
    public class employee_contactController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: employee_contact
        public async Task<ActionResult> Index()
        {
            var crm_employee_contact = db.crm_employee_contact.Include(c => c.crm_employee);
            return View(await crm_employee_contact.ToListAsync());
        }

        // GET: employee_contact/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_employee_contact crm_employee_contact = await db.crm_employee_contact.FindAsync(id);
            if (crm_employee_contact == null)
            {
                return HttpNotFound();
            }
            return View(crm_employee_contact);
        }

        // GET: employee_contact/Create
        public ActionResult Create()
        {
            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name");
            return View();
        }

        // POST: employee_contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,crm_employee_id,description")] crm_employee_contact crm_employee_contact)
        {
            if (ModelState.IsValid)
            {
                db.crm_employee_contact.Add(crm_employee_contact);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name", crm_employee_contact.crm_employee_id);
            return View(crm_employee_contact);
        }

        // GET: employee_contact/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_employee_contact crm_employee_contact = await db.crm_employee_contact.FindAsync(id);
            if (crm_employee_contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name", crm_employee_contact.crm_employee_id);
            return View(crm_employee_contact);
        }

        // POST: employee_contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,crm_employee_id,description")] crm_employee_contact crm_employee_contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crm_employee_contact).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name", crm_employee_contact.crm_employee_id);
            return View(crm_employee_contact);
        }

        // GET: employee_contact/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_employee_contact crm_employee_contact = await db.crm_employee_contact.FindAsync(id);
            if (crm_employee_contact == null)
            {
                return HttpNotFound();
            }
            return View(crm_employee_contact);
        }

        // POST: employee_contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            crm_employee_contact crm_employee_contact = await db.crm_employee_contact.FindAsync(id);
            db.crm_employee_contact.Remove(crm_employee_contact);
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
