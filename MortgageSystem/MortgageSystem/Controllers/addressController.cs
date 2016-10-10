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
    public class addressController : Controller
    {
        
        private mortgageEntities db = new mortgageEntities();

        // GET: address
        public async Task<ActionResult> Index()
        {
            return View(await db.crm_address.ToListAsync());
        }

        // GET: address/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_address crm_address = await db.crm_address.FindAsync(id);
            if (crm_address == null)
            {
                return HttpNotFound();
            }
            return View(crm_address);
        }

        // GET: address/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: address/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,description")] crm_address crm_address)
        {
            if (ModelState.IsValid)
            {
                db.crm_address.Add(crm_address);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(crm_address);
        }

        // GET: address/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_address crm_address = await db.crm_address.FindAsync(id);
            if (crm_address == null)
            {
                return HttpNotFound();
            }
            return View(crm_address);
        }

        // POST: address/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,description")] crm_address crm_address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crm_address).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(crm_address);
        }

        // GET: address/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_address crm_address = await db.crm_address.FindAsync(id);
            if (crm_address == null)
            {
                return HttpNotFound();
            }
            return View(crm_address);
        }

        // POST: address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            crm_address crm_address = await db.crm_address.FindAsync(id);
            db.crm_address.Remove(crm_address);
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
