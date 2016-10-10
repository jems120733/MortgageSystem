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
    public class assignmentController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: assignment
        public async Task<ActionResult> Index()
        {
            var crm_assignment = db.crm_assignment.Include(c => c.crm_branch).Include(c => c.crm_employee).Include(c => c.crm_job_position_tf).Include(c => c.mf_status);
            return View(await crm_assignment.ToListAsync());
        }

        // GET: assignment/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_assignment crm_assignment = await db.crm_assignment.FindAsync(id);
            if (crm_assignment == null)
            {
                return HttpNotFound();
            }
            return View(crm_assignment);
        }

        // GET: assignment/Create
        public ActionResult Create()
        {
            ViewBag.crm_branch_id = new SelectList(db.crm_branch, "id", "description");
            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name");
            ViewBag.crm_job_positon_tf_id = new SelectList(db.crm_job_position_tf, "id", "id");
            ViewBag.mf_status_id = new SelectList(db.mf_status, "id", "description");
            return View();
        }

        // POST: assignment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,crm_job_positon_tf_id,crm_branch_id,crm_employee_id,mf_status_id")] crm_assignment crm_assignment)
        {
            if (ModelState.IsValid)
            {
                db.crm_assignment.Add(crm_assignment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.crm_branch_id = new SelectList(db.crm_branch, "id", "description", crm_assignment.crm_branch_id);
            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name", crm_assignment.crm_employee_id);
            ViewBag.crm_job_positon_tf_id = new SelectList(db.crm_job_position_tf, "id", "id", crm_assignment.crm_job_positon_tf_id);
            ViewBag.mf_status_id = new SelectList(db.mf_status, "id", "description", crm_assignment.mf_status_id);
            return View(crm_assignment);
        }

        // GET: assignment/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_assignment crm_assignment = await db.crm_assignment.FindAsync(id);
            if (crm_assignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.crm_branch_id = new SelectList(db.crm_branch, "id", "description", crm_assignment.crm_branch_id);
            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name", crm_assignment.crm_employee_id);
            ViewBag.crm_job_positon_tf_id = new SelectList(db.crm_job_position_tf, "id", "id", crm_assignment.crm_job_positon_tf_id);
            ViewBag.mf_status_id = new SelectList(db.mf_status, "id", "description", crm_assignment.mf_status_id);
            return View(crm_assignment);
        }

        // POST: assignment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,crm_job_positon_tf_id,crm_branch_id,crm_employee_id,mf_status_id")] crm_assignment crm_assignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crm_assignment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.crm_branch_id = new SelectList(db.crm_branch, "id", "description", crm_assignment.crm_branch_id);
            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name", crm_assignment.crm_employee_id);
            ViewBag.crm_job_positon_tf_id = new SelectList(db.crm_job_position_tf, "id", "id", crm_assignment.crm_job_positon_tf_id);
            ViewBag.mf_status_id = new SelectList(db.mf_status, "id", "description", crm_assignment.mf_status_id);
            return View(crm_assignment);
        }

        // GET: assignment/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_assignment crm_assignment = await db.crm_assignment.FindAsync(id);
            if (crm_assignment == null)
            {
                return HttpNotFound();
            }
            return View(crm_assignment);
        }

        // POST: assignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            crm_assignment crm_assignment = await db.crm_assignment.FindAsync(id);
            db.crm_assignment.Remove(crm_assignment);
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
