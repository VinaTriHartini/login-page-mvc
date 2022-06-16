using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConnectSQLServer.Models;

namespace ConnectSQLServer.Controllers
{
    public class pemesanansController : Controller
    {
        private coffeeshopEntities1 db = new coffeeshopEntities1();

        // GET: pemesanans
        public ActionResult Index()
        {
            var pemesanans = db.pemesanans.Include(p => p.user);
            return View(pemesanans.ToList());
        }

        // GET: pemesanans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pemesanan pemesanan = db.pemesanans.Find(id);
            if (pemesanan == null)
            {
                return HttpNotFound();
            }
            return View(pemesanan);
        }

        // GET: pemesanans/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(db.users, "id", "username");
            return View();
        }

        // POST: pemesanans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,order_date,status,total_price,user_id")] pemesanan pemesanan)
        {
            if (ModelState.IsValid)
            {
                db.pemesanans.Add(pemesanan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.user_id = new SelectList(db.users, "id", "username", pemesanan.user_id);
            return View(pemesanan);
        }

        // GET: pemesanans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pemesanan pemesanan = db.pemesanans.Find(id);
            if (pemesanan == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.users, "id", "username", pemesanan.user_id);
            return View(pemesanan);
        }

        // POST: pemesanans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,order_date,status,total_price,user_id")] pemesanan pemesanan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pemesanan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.users, "id", "username", pemesanan.user_id);
            return View(pemesanan);
        }

        // GET: pemesanans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pemesanan pemesanan = db.pemesanans.Find(id);
            if (pemesanan == null)
            {
                return HttpNotFound();
            }
            return View(pemesanan);
        }

        // POST: pemesanans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pemesanan pemesanan = db.pemesanans.Find(id);
            db.pemesanans.Remove(pemesanan);
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
