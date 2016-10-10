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
    public class civilstatusController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: civilstatus
        public async Task<ActionResult> Index()
        {
            return View(await db.crm_civil_status.ToListAsync());
        }

        // GET: civilstatus/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_civil_status crm_civil_status = await db.crm_civil_status.FindAsync(id);
            if (crm_civil_status == null)
            {
                return HttpNotFound();
            }
            return View(crm_civil_status);
        }

        // GET: civilstatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: civilstatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,description")] crm_civil_status crm_civil_status)
        {
            if (ModelState.IsValid)
            {
                db.crm_civil_status.Add(crm_civil_status);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(crm_civil_status);
        }

        // GET: civilstatus/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_civil_status crm_civil_status = await db.crm_civil_status.FindAsync(id);
            if (crm_civil_status == null)
            {
                return HttpNotFound();
            }
            return View(crm_civil_status);
        }

        // POST: civilstatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,description")] crm_civil_status crm_civil_status)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crm_civil_status).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(crm_civil_status);
        }

        // GET: civilstatus/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_civil_status crm_civil_status = await db.crm_civil_status.FindAsync(id);
            if (crm_civil_status == null)
            {
                return HttpNotFound();
            }
            return View(crm_civil_status);
        }

        // POST: civilstatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            crm_civil_status crm_civil_status = await db.crm_civil_status.FindAsync(id);
            db.crm_civil_status.Remove(crm_civil_status);
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
