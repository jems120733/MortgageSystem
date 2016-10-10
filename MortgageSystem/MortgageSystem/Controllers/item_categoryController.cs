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
    public class item_categoryController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: item_category
        public async Task<ActionResult> Index()
        {
            var inv_item_category = db.inv_item_category.Include(i => i.inv_item_category2);
            return View(await inv_item_category.ToListAsync());
        }

        // GET: item_category/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv_item_category inv_item_category = await db.inv_item_category.FindAsync(id);
            if (inv_item_category == null)
            {
                return HttpNotFound();
            }
            return View(inv_item_category);
        }

        // GET: item_category/Create
        public ActionResult Create()
        {
            ViewBag.item_category_id = new SelectList(db.inv_item_category, "id", "description");
            return View();
        }

        // POST: item_category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,description,item_category_id")] inv_item_category inv_item_category)
        {
            if (ModelState.IsValid)
            {
                db.inv_item_category.Add(inv_item_category);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.item_category_id = new SelectList(db.inv_item_category, "id", "description", inv_item_category.item_category_id);
            return View(inv_item_category);
        }

        // GET: item_category/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv_item_category inv_item_category = await db.inv_item_category.FindAsync(id);
            if (inv_item_category == null)
            {
                return HttpNotFound();
            }
            ViewBag.item_category_id = new SelectList(db.inv_item_category, "id", "description", inv_item_category.item_category_id);
            return View(inv_item_category);
        }

        // POST: item_category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,description,item_category_id")] inv_item_category inv_item_category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inv_item_category).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.item_category_id = new SelectList(db.inv_item_category, "id", "description", inv_item_category.item_category_id);
            return View(inv_item_category);
        }

        // GET: item_category/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv_item_category inv_item_category = await db.inv_item_category.FindAsync(id);
            if (inv_item_category == null)
            {
                return HttpNotFound();
            }
            return View(inv_item_category);
        }

        // POST: item_category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            inv_item_category inv_item_category = await db.inv_item_category.FindAsync(id);
            db.inv_item_category.Remove(inv_item_category);
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
