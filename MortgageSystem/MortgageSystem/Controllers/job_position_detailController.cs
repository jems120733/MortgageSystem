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
    public class job_position_detailController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: job_position_detail
        public async Task<ActionResult> Index()
        {
            var crm_job_position_tf = db.crm_job_position_tf.Include(c => c.crm_employee).Include(c => c.crm_job_position_mf);
            return View(await crm_job_position_tf.ToListAsync());
        }

        // GET: job_position_detail/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_job_position_tf crm_job_position_tf = await db.crm_job_position_tf.FindAsync(id);
            if (crm_job_position_tf == null)
            {
                return HttpNotFound();
            }
            return View(crm_job_position_tf);
        }

        // GET: job_position_detail/Create
        public ActionResult Create()
        {
            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name");
            ViewBag.crm_job_position_mf_id = new SelectList(db.crm_job_position_mf, "id", "description");
            return View();
        }

        // POST: job_position_detail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,crm_employee_id,crm_job_position_mf_id,date_hired,date_end")] crm_job_position_tf crm_job_position_tf)
        {
            if (ModelState.IsValid)
            {
                db.crm_job_position_tf.Add(crm_job_position_tf);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name", crm_job_position_tf.crm_employee_id);
            ViewBag.crm_job_position_mf_id = new SelectList(db.crm_job_position_mf, "id", "description", crm_job_position_tf.crm_job_position_mf_id);
            return View(crm_job_position_tf);
        }

        // GET: job_position_detail/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_job_position_tf crm_job_position_tf = await db.crm_job_position_tf.FindAsync(id);
            if (crm_job_position_tf == null)
            {
                return HttpNotFound();
            }
            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name", crm_job_position_tf.crm_employee_id);
            ViewBag.crm_job_position_mf_id = new SelectList(db.crm_job_position_mf, "id", "description", crm_job_position_tf.crm_job_position_mf_id);
            return View(crm_job_position_tf);
        }

        // POST: job_position_detail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,crm_employee_id,crm_job_position_mf_id,date_hired,date_end")] crm_job_position_tf crm_job_position_tf)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crm_job_position_tf).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name", crm_job_position_tf.crm_employee_id);
            ViewBag.crm_job_position_mf_id = new SelectList(db.crm_job_position_mf, "id", "description", crm_job_position_tf.crm_job_position_mf_id);
            return View(crm_job_position_tf);
        }

        // GET: job_position_detail/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_job_position_tf crm_job_position_tf = await db.crm_job_position_tf.FindAsync(id);
            if (crm_job_position_tf == null)
            {
                return HttpNotFound();
            }
            return View(crm_job_position_tf);
        }

        // POST: job_position_detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            crm_job_position_tf crm_job_position_tf = await db.crm_job_position_tf.FindAsync(id);
            db.crm_job_position_tf.Remove(crm_job_position_tf);
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
