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
    public class job_position_mfController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: job_position_mf
        public async Task<ActionResult> Index()
        {
            return View(await db.crm_job_position_mf.ToListAsync());
        }

        // GET: job_position_mf/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_job_position_mf crm_job_position_mf = await db.crm_job_position_mf.FindAsync(id);
            if (crm_job_position_mf == null)
            {
                return HttpNotFound();
            }
            return View(crm_job_position_mf);
        }

        // GET: job_position_mf/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: job_position_mf/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,description")] crm_job_position_mf crm_job_position_mf)
        {
            if (ModelState.IsValid)
            {
                db.crm_job_position_mf.Add(crm_job_position_mf);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(crm_job_position_mf);
        }

        // GET: job_position_mf/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_job_position_mf crm_job_position_mf = await db.crm_job_position_mf.FindAsync(id);
            if (crm_job_position_mf == null)
            {
                return HttpNotFound();
            }
            return View(crm_job_position_mf);
        }

        // POST: job_position_mf/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,description")] crm_job_position_mf crm_job_position_mf)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crm_job_position_mf).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(crm_job_position_mf);
        }

        // GET: job_position_mf/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_job_position_mf crm_job_position_mf = await db.crm_job_position_mf.FindAsync(id);
            if (crm_job_position_mf == null)
            {
                return HttpNotFound();
            }
            return View(crm_job_position_mf);
        }

        // POST: job_position_mf/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            crm_job_position_mf crm_job_position_mf = await db.crm_job_position_mf.FindAsync(id);
            db.crm_job_position_mf.Remove(crm_job_position_mf);
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
