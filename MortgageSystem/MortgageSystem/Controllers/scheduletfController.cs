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
    public class scheduletfController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: scheduletf
        public async Task<ActionResult> Index()
        {
            var crm_seminar_scheduler_tf = db.crm_seminar_scheduler_tf.Include(c => c.crm_customer).Include(c => c.crm_seminar_scheduler_mf).Include(c => c.mf_status);
            return View(await crm_seminar_scheduler_tf.ToListAsync());
        }

        // GET: scheduletf/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_seminar_scheduler_tf crm_seminar_scheduler_tf = await db.crm_seminar_scheduler_tf.FindAsync(id);
            if (crm_seminar_scheduler_tf == null)
            {
                return HttpNotFound();
            }
            return View(crm_seminar_scheduler_tf);
        }

        // GET: scheduletf/Create
        public ActionResult Create()
        {
            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name");
            ViewBag.crm_seminar_scheduler_mf_id = new SelectList(db.crm_seminar_scheduler_mf, "id", "description");
            ViewBag.mf_status_id = new SelectList(db.mf_status, "id", "description");
            return View();
        }

        // POST: scheduletf/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,crm_seminar_scheduler_mf_id,crm_customer_id,mf_status_id")] crm_seminar_scheduler_tf crm_seminar_scheduler_tf)
        {
            if (ModelState.IsValid)
            {
                db.crm_seminar_scheduler_tf.Add(crm_seminar_scheduler_tf);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name", crm_seminar_scheduler_tf.crm_customer_id);
            ViewBag.crm_seminar_scheduler_mf_id = new SelectList(db.crm_seminar_scheduler_mf, "id", "description", crm_seminar_scheduler_tf.crm_seminar_scheduler_mf_id);
            ViewBag.mf_status_id = new SelectList(db.mf_status, "id", "description", crm_seminar_scheduler_tf.mf_status_id);
            return View(crm_seminar_scheduler_tf);
        }

        // GET: scheduletf/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_seminar_scheduler_tf crm_seminar_scheduler_tf = await db.crm_seminar_scheduler_tf.FindAsync(id);
            if (crm_seminar_scheduler_tf == null)
            {
                return HttpNotFound();
            }
            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name", crm_seminar_scheduler_tf.crm_customer_id);
            ViewBag.crm_seminar_scheduler_mf_id = new SelectList(db.crm_seminar_scheduler_mf, "id", "description", crm_seminar_scheduler_tf.crm_seminar_scheduler_mf_id);
            ViewBag.mf_status_id = new SelectList(db.mf_status, "id", "description", crm_seminar_scheduler_tf.mf_status_id);
            return View(crm_seminar_scheduler_tf);
        }

        // POST: scheduletf/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,crm_seminar_scheduler_mf_id,crm_customer_id,mf_status_id")] crm_seminar_scheduler_tf crm_seminar_scheduler_tf)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crm_seminar_scheduler_tf).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name", crm_seminar_scheduler_tf.crm_customer_id);
            ViewBag.crm_seminar_scheduler_mf_id = new SelectList(db.crm_seminar_scheduler_mf, "id", "description", crm_seminar_scheduler_tf.crm_seminar_scheduler_mf_id);
            ViewBag.mf_status_id = new SelectList(db.mf_status, "id", "description", crm_seminar_scheduler_tf.mf_status_id);
            return View(crm_seminar_scheduler_tf);
        }

        // GET: scheduletf/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_seminar_scheduler_tf crm_seminar_scheduler_tf = await db.crm_seminar_scheduler_tf.FindAsync(id);
            if (crm_seminar_scheduler_tf == null)
            {
                return HttpNotFound();
            }
            return View(crm_seminar_scheduler_tf);
        }

        // POST: scheduletf/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            crm_seminar_scheduler_tf crm_seminar_scheduler_tf = await db.crm_seminar_scheduler_tf.FindAsync(id);
            db.crm_seminar_scheduler_tf.Remove(crm_seminar_scheduler_tf);
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
