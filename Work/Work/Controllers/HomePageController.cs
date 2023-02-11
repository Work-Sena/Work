using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Work.Models.Entity;

namespace Work.Controllers
{
    [AllowAnonymous]
    public class HomePageController : Controller
    {
        private WorkDbEntities db = new WorkDbEntities();

        // GET: HomePage
        public ActionResult Index()
        {
            ViewBag.guid = Convert.ToString( Session["Guid"]);
            return View(db.Tbl_Employee.ToList());
        }

        // GET: HomePage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Employee tbl_Employee = db.Tbl_Employee.Find(id);
            if (tbl_Employee == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Employee);
        }

        // GET: HomePage/Create
        [Authorize(Roles ="A")] //rolü A olan
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomePage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Guid,Name,Surname,Email,Password,Unvan,Role")] Tbl_Employee tbl_Employee)
        {
            if (ModelState.IsValid)
            {
                tbl_Employee.Guid = Guid.NewGuid();
                db.Tbl_Employee.Add(tbl_Employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Employee);
        }

        // GET: HomePage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Employee tbl_Employee = db.Tbl_Employee.Find(id);
            if (tbl_Employee == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Employee);
        }

        // POST: HomePage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,Email,Password,Unvan,Role")] Tbl_Employee tbl_Employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Employee);
        }

        // GET: HomePage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Employee tbl_Employee = db.Tbl_Employee.Find(id);
            if (tbl_Employee == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Employee);
        }

        // POST: HomePage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Employee tbl_Employee = db.Tbl_Employee.Find(id);
            db.Tbl_Employee.Remove(tbl_Employee);
            db.SaveChanges();
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
