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
    public class contactController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: contact
        public async Task<ActionResult> Index()
        {
            return View(await db.crm_contact.ToListAsync());
        }

        // GET: contact/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_contact crm_contact = await db.crm_contact.FindAsync(id);
            if (crm_contact == null)
            {
                return HttpNotFound();
            }
            return View(crm_contact);
        }

        // GET: contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,description")] crm_contact crm_contact)
        {
            if (ModelState.IsValid)
            {
                db.crm_contact.Add(crm_contact);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(crm_contact);
        }

        // GET: contact/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_contact crm_contact = await db.crm_contact.FindAsync(id);
            if (crm_contact == null)
            {
                return HttpNotFound();
            }
            return View(crm_contact);
        }

        // POST: contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,description")] crm_contact crm_contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crm_contact).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(crm_contact);
        }

        // GET: contact/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_contact crm_contact = await db.crm_contact.FindAsync(id);
            if (crm_contact == null)
            {
                return HttpNotFound();
            }
            return View(crm_contact);
        }

        // POST: contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            crm_contact crm_contact = await db.crm_contact.FindAsync(id);
            db.crm_contact.Remove(crm_contact);
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
