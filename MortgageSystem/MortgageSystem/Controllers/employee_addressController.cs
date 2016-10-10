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
    public class employee_addressController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: employee_address
        public async Task<ActionResult> Index()
        {
            var crm_employee_address = db.crm_employee_address.Include(c => c.crm_address).Include(c => c.crm_employee);
            return View(await crm_employee_address.ToListAsync());
        }

        // GET: employee_address/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_employee_address crm_employee_address = await db.crm_employee_address.FindAsync(id);
            if (crm_employee_address == null)
            {
                return HttpNotFound();
            }
            return View(crm_employee_address);
        }

        // GET: employee_address/Create
        public ActionResult Create()
        {
            ViewBag.crm_address_id = new SelectList(db.crm_address, "id", "description");
            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name");
            return View();
        }

        // POST: employee_address/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,crm_employee_id,crm_address_id")] crm_employee_address crm_employee_address)
        {
            if (ModelState.IsValid)
            {
                db.crm_employee_address.Add(crm_employee_address);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.crm_address_id = new SelectList(db.crm_address, "id", "description", crm_employee_address.crm_address_id);
            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name", crm_employee_address.crm_employee_id);
            return View(crm_employee_address);
        }

        // GET: employee_address/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_employee_address crm_employee_address = await db.crm_employee_address.FindAsync(id);
            if (crm_employee_address == null)
            {
                return HttpNotFound();
            }
            ViewBag.crm_address_id = new SelectList(db.crm_address, "id", "description", crm_employee_address.crm_address_id);
            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name", crm_employee_address.crm_employee_id);
            return View(crm_employee_address);
        }

        // POST: employee_address/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,crm_employee_id,crm_address_id")] crm_employee_address crm_employee_address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crm_employee_address).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.crm_address_id = new SelectList(db.crm_address, "id", "description", crm_employee_address.crm_address_id);
            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name", crm_employee_address.crm_employee_id);
            return View(crm_employee_address);
        }

        // GET: employee_address/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_employee_address crm_employee_address = await db.crm_employee_address.FindAsync(id);
            if (crm_employee_address == null)
            {
                return HttpNotFound();
            }
            return View(crm_employee_address);
        }

        // POST: employee_address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            crm_employee_address crm_employee_address = await db.crm_employee_address.FindAsync(id);
            db.crm_employee_address.Remove(crm_employee_address);
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
