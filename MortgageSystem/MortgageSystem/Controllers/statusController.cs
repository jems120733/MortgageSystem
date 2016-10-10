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
    public class statusController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: status
        public async Task<ActionResult> Index()
        {
            return View(await db.mf_status.ToListAsync());
        }

        // GET: status/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mf_status mf_status = await db.mf_status.FindAsync(id);
            if (mf_status == null)
            {
                return HttpNotFound();
            }
            return View(mf_status);
        }

        // GET: status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: status/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,description")] mf_status mf_status)
        {
            if (ModelState.IsValid)
            {
                db.mf_status.Add(mf_status);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(mf_status);
        }

        // GET: status/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mf_status mf_status = await db.mf_status.FindAsync(id);
            if (mf_status == null)
            {
                return HttpNotFound();
            }
            return View(mf_status);
        }

        // POST: status/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,description")] mf_status mf_status)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mf_status).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mf_status);
        }

        // GET: status/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mf_status mf_status = await db.mf_status.FindAsync(id);
            if (mf_status == null)
            {
                return HttpNotFound();
            }
            return View(mf_status);
        }

        // POST: status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            mf_status mf_status = await db.mf_status.FindAsync(id);
            db.mf_status.Remove(mf_status);
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
