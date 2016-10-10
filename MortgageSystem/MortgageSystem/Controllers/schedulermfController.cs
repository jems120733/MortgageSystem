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
    public class schedulermfController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: schedulermf
        public async Task<ActionResult> Index()
        {
            var crm_seminar_scheduler_mf = db.crm_seminar_scheduler_mf.Include(c => c.crm_employee).Include(c => c.crm_user);
            return View(await crm_seminar_scheduler_mf.ToListAsync());
        }

        // GET: schedulermf/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_seminar_scheduler_mf crm_seminar_scheduler_mf = await db.crm_seminar_scheduler_mf.FindAsync(id);
            if (crm_seminar_scheduler_mf == null)
            {
                return HttpNotFound();
            }
            return View(crm_seminar_scheduler_mf);
        }

        // GET: schedulermf/Create
        public ActionResult Create()
        {
            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name");
            ViewBag.crm_user_id = new SelectList(db.crm_user, "id", "username");
            return View();
        }

        // POST: schedulermf/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,scheduled_date,crm_employee_id,crm_user_id,description")] crm_seminar_scheduler_mf crm_seminar_scheduler_mf)
        {
            if (ModelState.IsValid)
            {
                db.crm_seminar_scheduler_mf.Add(crm_seminar_scheduler_mf);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name", crm_seminar_scheduler_mf.crm_employee_id);
            ViewBag.crm_user_id = new SelectList(db.crm_user, "id", "username", crm_seminar_scheduler_mf.crm_user_id);
            return View(crm_seminar_scheduler_mf);
        }

        // GET: schedulermf/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_seminar_scheduler_mf crm_seminar_scheduler_mf = await db.crm_seminar_scheduler_mf.FindAsync(id);
            if (crm_seminar_scheduler_mf == null)
            {
                return HttpNotFound();
            }
            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name", crm_seminar_scheduler_mf.crm_employee_id);
            ViewBag.crm_user_id = new SelectList(db.crm_user, "id", "username", crm_seminar_scheduler_mf.crm_user_id);
            return View(crm_seminar_scheduler_mf);
        }

        // POST: schedulermf/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,scheduled_date,crm_employee_id,crm_user_id,description")] crm_seminar_scheduler_mf crm_seminar_scheduler_mf)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crm_seminar_scheduler_mf).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name", crm_seminar_scheduler_mf.crm_employee_id);
            ViewBag.crm_user_id = new SelectList(db.crm_user, "id", "username", crm_seminar_scheduler_mf.crm_user_id);
            return View(crm_seminar_scheduler_mf);
        }

        // GET: schedulermf/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_seminar_scheduler_mf crm_seminar_scheduler_mf = await db.crm_seminar_scheduler_mf.FindAsync(id);
            if (crm_seminar_scheduler_mf == null)
            {
                return HttpNotFound();
            }
            return View(crm_seminar_scheduler_mf);
        }

        // POST: schedulermf/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            crm_seminar_scheduler_mf crm_seminar_scheduler_mf = await db.crm_seminar_scheduler_mf.FindAsync(id);
            db.crm_seminar_scheduler_mf.Remove(crm_seminar_scheduler_mf);
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
