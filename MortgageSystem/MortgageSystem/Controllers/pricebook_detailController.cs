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
    public class pricebook_detailController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: pricebook_detail
        public async Task<ActionResult> Index()
        {
            var inv_pricebook_detail = db.inv_pricebook_detail.Include(i => i.inv_pricebook_header).Include(i => i.inv_uom).Include(i => i.inv_item).Include(i => i.mf_status);
            return View(await inv_pricebook_detail.ToListAsync());
        }

        // GET: pricebook_detail/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv_pricebook_detail inv_pricebook_detail = await db.inv_pricebook_detail.FindAsync(id);
            if (inv_pricebook_detail == null)
            {
                return HttpNotFound();
            }
            return View(inv_pricebook_detail);
        }

        // GET: pricebook_detail/Create
        public ActionResult Create()
        {
            ViewBag.pricebook_header_id = new SelectList(db.inv_pricebook_header, "id", "description");
            ViewBag.uom_id = new SelectList(db.inv_uom, "id", "description");
            ViewBag.item_id = new SelectList(db.inv_item, "id", "short_description");
            ViewBag.status_id = new SelectList(db.mf_status, "id", "description");
            return View();
        }

        // POST: pricebook_detail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,pricebook_header_id,item_id,uom_id,status_id,price_vat_ex,price_vat_in,upc,sku")] inv_pricebook_detail inv_pricebook_detail)
        {
            if (ModelState.IsValid)
            {
                db.inv_pricebook_detail.Add(inv_pricebook_detail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.pricebook_header_id = new SelectList(db.inv_pricebook_header, "id", "description", inv_pricebook_detail.pricebook_header_id);
            ViewBag.uom_id = new SelectList(db.inv_uom, "id", "description", inv_pricebook_detail.uom_id);
            ViewBag.item_id = new SelectList(db.inv_item, "id", "short_description", inv_pricebook_detail.item_id);
            ViewBag.status_id = new SelectList(db.mf_status, "id", "description", inv_pricebook_detail.status_id);
            return View(inv_pricebook_detail);
        }

        // GET: pricebook_detail/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv_pricebook_detail inv_pricebook_detail = await db.inv_pricebook_detail.FindAsync(id);
            if (inv_pricebook_detail == null)
            {
                return HttpNotFound();
            }
            ViewBag.pricebook_header_id = new SelectList(db.inv_pricebook_header, "id", "description", inv_pricebook_detail.pricebook_header_id);
            ViewBag.uom_id = new SelectList(db.inv_uom, "id", "description", inv_pricebook_detail.uom_id);
            ViewBag.item_id = new SelectList(db.inv_item, "id", "short_description", inv_pricebook_detail.item_id);
            ViewBag.status_id = new SelectList(db.mf_status, "id", "description", inv_pricebook_detail.status_id);
            return View(inv_pricebook_detail);
        }

        // POST: pricebook_detail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,pricebook_header_id,item_id,uom_id,status_id,price_vat_ex,price_vat_in,upc,sku")] inv_pricebook_detail inv_pricebook_detail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inv_pricebook_detail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.pricebook_header_id = new SelectList(db.inv_pricebook_header, "id", "description", inv_pricebook_detail.pricebook_header_id);
            ViewBag.uom_id = new SelectList(db.inv_uom, "id", "description", inv_pricebook_detail.uom_id);
            ViewBag.item_id = new SelectList(db.inv_item, "id", "short_description", inv_pricebook_detail.item_id);
            ViewBag.status_id = new SelectList(db.mf_status, "id", "description", inv_pricebook_detail.status_id);
            return View(inv_pricebook_detail);
        }

        // GET: pricebook_detail/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv_pricebook_detail inv_pricebook_detail = await db.inv_pricebook_detail.FindAsync(id);
            if (inv_pricebook_detail == null)
            {
                return HttpNotFound();
            }
            return View(inv_pricebook_detail);
        }

        // POST: pricebook_detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            inv_pricebook_detail inv_pricebook_detail = await db.inv_pricebook_detail.FindAsync(id);
            db.inv_pricebook_detail.Remove(inv_pricebook_detail);
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
