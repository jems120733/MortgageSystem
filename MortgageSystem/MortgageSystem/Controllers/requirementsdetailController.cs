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
    public class requirementsdetailController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: requirementsdetail
        public async Task<ActionResult> Index()
        {
            var crm_requirements_tf = db.crm_requirements_tf.Include(c => c.crm_branch).Include(c => c.crm_customer).Include(c => c.crm_user).Include(c => c.mf_status);
            return View(await crm_requirements_tf.ToListAsync());
        }

        // GET: requirementsdetail/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_requirements_tf crm_requirements_tf = await db.crm_requirements_tf.FindAsync(id);
            if (crm_requirements_tf == null)
            {
                return HttpNotFound();
            }
            return View(crm_requirements_tf);
        }

        // GET: requirementsdetail/Create
        public ActionResult Create()
        {
            ViewBag.crm_branch_id = new SelectList(db.crm_branch, "id", "description");
            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name");
            ViewBag.crm_user_id = new SelectList(db.crm_user, "id", "username");
            ViewBag.mf_status_id = new SelectList(db.mf_status, "id", "description");
            return View();
        }

        // POST: requirementsdetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,crm_customer_id,crm_branch_id,crm_user_id,mf_status_id,transaction_date,remarks")] crm_requirements_tf crm_requirements_tf)
        {
            if (ModelState.IsValid)
            {
                db.crm_requirements_tf.Add(crm_requirements_tf);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.crm_branch_id = new SelectList(db.crm_branch, "id", "description", crm_requirements_tf.crm_branch_id);
            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name", crm_requirements_tf.crm_customer_id);
            ViewBag.crm_user_id = new SelectList(db.crm_user, "id", "username", crm_requirements_tf.crm_user_id);
            ViewBag.mf_status_id = new SelectList(db.mf_status, "id", "description", crm_requirements_tf.mf_status_id);
            return View(crm_requirements_tf);
        }

        // GET: requirementsdetail/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_requirements_tf crm_requirements_tf = await db.crm_requirements_tf.FindAsync(id);
            if (crm_requirements_tf == null)
            {
                return HttpNotFound();
            }
            ViewBag.crm_branch_id = new SelectList(db.crm_branch, "id", "description", crm_requirements_tf.crm_branch_id);
            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name", crm_requirements_tf.crm_customer_id);
            ViewBag.crm_user_id = new SelectList(db.crm_user, "id", "username", crm_requirements_tf.crm_user_id);
            ViewBag.mf_status_id = new SelectList(db.mf_status, "id", "description", crm_requirements_tf.mf_status_id);
            return View(crm_requirements_tf);
        }

        // POST: requirementsdetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,crm_customer_id,crm_branch_id,crm_user_id,mf_status_id,transaction_date,remarks")] crm_requirements_tf crm_requirements_tf)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crm_requirements_tf).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.crm_branch_id = new SelectList(db.crm_branch, "id", "description", crm_requirements_tf.crm_branch_id);
            ViewBag.crm_customer_id = new SelectList(db.crm_customer, "id", "first_name", crm_requirements_tf.crm_customer_id);
            ViewBag.crm_user_id = new SelectList(db.crm_user, "id", "username", crm_requirements_tf.crm_user_id);
            ViewBag.mf_status_id = new SelectList(db.mf_status, "id", "description", crm_requirements_tf.mf_status_id);
            return View(crm_requirements_tf);
        }

        // GET: requirementsdetail/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_requirements_tf crm_requirements_tf = await db.crm_requirements_tf.FindAsync(id);
            if (crm_requirements_tf == null)
            {
                return HttpNotFound();
            }
            return View(crm_requirements_tf);
        }

        // POST: requirementsdetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            crm_requirements_tf crm_requirements_tf = await db.crm_requirements_tf.FindAsync(id);
            db.crm_requirements_tf.Remove(crm_requirements_tf);
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
