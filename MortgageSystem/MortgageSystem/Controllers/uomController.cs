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
    public class uomController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: uom
        public async Task<ActionResult> Index()
        {
            return View(await db.inv_uom.ToListAsync());
        }

        // GET: uom/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv_uom inv_uom = await db.inv_uom.FindAsync(id);
            if (inv_uom == null)
            {
                return HttpNotFound();
            }
            return View(inv_uom);
        }

        // GET: uom/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: uom/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,description")] inv_uom inv_uom)
        {
            if (ModelState.IsValid)
            {
                db.inv_uom.Add(inv_uom);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(inv_uom);
        }

        // GET: uom/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv_uom inv_uom = await db.inv_uom.FindAsync(id);
            if (inv_uom == null)
            {
                return HttpNotFound();
            }
            return View(inv_uom);
        }

        // POST: uom/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,description")] inv_uom inv_uom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inv_uom).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(inv_uom);
        }

        // GET: uom/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv_uom inv_uom = await db.inv_uom.FindAsync(id);
            if (inv_uom == null)
            {
                return HttpNotFound();
            }
            return View(inv_uom);
        }

        // POST: uom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            inv_uom inv_uom = await db.inv_uom.FindAsync(id);
            db.inv_uom.Remove(inv_uom);
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
