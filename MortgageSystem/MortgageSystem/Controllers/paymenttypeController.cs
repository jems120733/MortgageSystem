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
    public class paymenttypeController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: paymenttype
        public async Task<ActionResult> Index()
        {
            return View(await db.mf_payment_type.ToListAsync());
        }

        // GET: paymenttype/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mf_payment_type mf_payment_type = await db.mf_payment_type.FindAsync(id);
            if (mf_payment_type == null)
            {
                return HttpNotFound();
            }
            return View(mf_payment_type);
        }

        // GET: paymenttype/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: paymenttype/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Description")] mf_payment_type mf_payment_type)
        {
            if (ModelState.IsValid)
            {
                db.mf_payment_type.Add(mf_payment_type);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(mf_payment_type);
        }

        // GET: paymenttype/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mf_payment_type mf_payment_type = await db.mf_payment_type.FindAsync(id);
            if (mf_payment_type == null)
            {
                return HttpNotFound();
            }
            return View(mf_payment_type);
        }

        // POST: paymenttype/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Description")] mf_payment_type mf_payment_type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mf_payment_type).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mf_payment_type);
        }

        // GET: paymenttype/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mf_payment_type mf_payment_type = await db.mf_payment_type.FindAsync(id);
            if (mf_payment_type == null)
            {
                return HttpNotFound();
            }
            return View(mf_payment_type);
        }

        // POST: paymenttype/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            mf_payment_type mf_payment_type = await db.mf_payment_type.FindAsync(id);
            db.mf_payment_type.Remove(mf_payment_type);
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
