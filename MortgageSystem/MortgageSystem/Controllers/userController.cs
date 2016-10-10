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
    public class userController : Controller
    {
        private mortgageEntities db = new mortgageEntities();

        // GET: user
        public async Task<ActionResult> Index()
        {
            var crm_user = db.crm_user.Include(c => c.crm_employee);
            return View(await crm_user.ToListAsync());
        }

        // GET: user/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_user crm_user = await db.crm_user.FindAsync(id);
            if (crm_user == null)
            {
                return HttpNotFound();
            }
            return View(crm_user);
        }

        // GET: user/Create
        public ActionResult Create()
        {
            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name");
            return View();
        }

        // POST: user/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,crm_employee_id,username,password")] crm_user crm_user)
        {
            if (ModelState.IsValid)
            {
                db.crm_user.Add(crm_user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name", crm_user.crm_employee_id);
            return View(crm_user);
        }

        // GET: user/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_user crm_user = await db.crm_user.FindAsync(id);
            if (crm_user == null)
            {
                return HttpNotFound();
            }
            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name", crm_user.crm_employee_id);
            return View(crm_user);
        }

        // POST: user/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,crm_employee_id,username,password")] crm_user crm_user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crm_user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.crm_employee_id = new SelectList(db.crm_employee, "id", "first_name", crm_user.crm_employee_id);
            return View(crm_user);
        }

        // GET: user/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_user crm_user = await db.crm_user.FindAsync(id);
            if (crm_user == null)
            {
                return HttpNotFound();
            }
            return View(crm_user);
        }

        // POST: user/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            crm_user crm_user = await db.crm_user.FindAsync(id);
            db.crm_user.Remove(crm_user);
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
