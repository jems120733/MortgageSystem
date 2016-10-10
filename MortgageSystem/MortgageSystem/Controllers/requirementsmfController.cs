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
    public class requirementsmfController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: requirementsmf
        public async Task<ActionResult> Index()
        {
            return View(await db.crm_requirements_mf.ToListAsync());
        }

        // GET: requirementsmf/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_requirements_mf crm_requirements_mf = await db.crm_requirements_mf.FindAsync(id);
            if (crm_requirements_mf == null)
            {
                return HttpNotFound();
            }
            return View(crm_requirements_mf);
        }

        // GET: requirementsmf/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: requirementsmf/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,description")] crm_requirements_mf crm_requirements_mf)
        {
            if (ModelState.IsValid)
            {
                db.crm_requirements_mf.Add(crm_requirements_mf);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(crm_requirements_mf);
        }

        // GET: requirementsmf/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_requirements_mf crm_requirements_mf = await db.crm_requirements_mf.FindAsync(id);
            if (crm_requirements_mf == null)
            {
                return HttpNotFound();
            }
            return View(crm_requirements_mf);
        }

        // POST: requirementsmf/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,description")] crm_requirements_mf crm_requirements_mf)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crm_requirements_mf).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(crm_requirements_mf);
        }

        // GET: requirementsmf/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_requirements_mf crm_requirements_mf = await db.crm_requirements_mf.FindAsync(id);
            if (crm_requirements_mf == null)
            {
                return HttpNotFound();
            }
            return View(crm_requirements_mf);
        }

        // POST: requirementsmf/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            crm_requirements_mf crm_requirements_mf = await db.crm_requirements_mf.FindAsync(id);
            db.crm_requirements_mf.Remove(crm_requirements_mf);
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
