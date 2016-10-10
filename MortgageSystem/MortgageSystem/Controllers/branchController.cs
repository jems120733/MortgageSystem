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
    public class branchController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: branch
        public async Task<ActionResult> Index()
        {
            return View(await db.crm_branch.ToListAsync());
        }

        // GET: branch/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_branch crm_branch = await db.crm_branch.FindAsync(id);
            if (crm_branch == null)
            {
                return HttpNotFound();
            }
            return View(crm_branch);
        }

        // GET: branch/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: branch/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,description")] crm_branch crm_branch)
        {
            if (ModelState.IsValid)
            {
                db.crm_branch.Add(crm_branch);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(crm_branch);
        }

        // GET: branch/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_branch crm_branch = await db.crm_branch.FindAsync(id);
            if (crm_branch == null)
            {
                return HttpNotFound();
            }
            return View(crm_branch);
        }

        // POST: branch/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,description")] crm_branch crm_branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crm_branch).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(crm_branch);
        }

        // GET: branch/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_branch crm_branch = await db.crm_branch.FindAsync(id);
            if (crm_branch == null)
            {
                return HttpNotFound();
            }
            return View(crm_branch);
        }

        // POST: branch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            crm_branch crm_branch = await db.crm_branch.FindAsync(id);
            db.crm_branch.Remove(crm_branch);
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
