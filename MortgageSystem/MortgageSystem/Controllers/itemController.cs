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
    public class itemController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: item
        public async Task<ActionResult> Index()
        {
            var inv_item = db.inv_item.Include(i => i.crm_branch).Include(i => i.mf_status);
            return View(await inv_item.ToListAsync());
        }

        // GET: item/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv_item inv_item = await db.inv_item.FindAsync(id);
            if (inv_item == null)
            {
                return HttpNotFound();
            }
            return View(inv_item);
        }

        // GET: item/Create
        public ActionResult Create()
        {
            ViewBag.branch_id = new SelectList(db.crm_branch, "id", "description");
            ViewBag.status_id = new SelectList(db.mf_status, "id", "description");
            return View();
        }

        // POST: item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,short_description,full_description,status_id,branch_id")] inv_item inv_item)
        {
            if (ModelState.IsValid)
            {
                db.inv_item.Add(inv_item);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.branch_id = new SelectList(db.crm_branch, "id", "description", inv_item.branch_id);
            ViewBag.status_id = new SelectList(db.mf_status, "id", "description", inv_item.status_id);
            return View(inv_item);
        }

        // GET: item/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv_item inv_item = await db.inv_item.FindAsync(id);
            if (inv_item == null)
            {
                return HttpNotFound();
            }
            ViewBag.branch_id = new SelectList(db.crm_branch, "id", "description", inv_item.branch_id);
            ViewBag.status_id = new SelectList(db.mf_status, "id", "description", inv_item.status_id);
            return View(inv_item);
        }

        // POST: item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,short_description,full_description,status_id,branch_id")] inv_item inv_item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inv_item).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.branch_id = new SelectList(db.crm_branch, "id", "description", inv_item.branch_id);
            ViewBag.status_id = new SelectList(db.mf_status, "id", "description", inv_item.status_id);
            return View(inv_item);
        }

        // GET: item/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv_item inv_item = await db.inv_item.FindAsync(id);
            if (inv_item == null)
            {
                return HttpNotFound();
            }
            return View(inv_item);
        }

        // POST: item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            inv_item inv_item = await db.inv_item.FindAsync(id);
            db.inv_item.Remove(inv_item);
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
