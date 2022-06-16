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
    public class orderdetailsController : Controller
    {
        private coffeeshopEntities1 db = new coffeeshopEntities1();

        // GET: orderdetails
        public ActionResult Index()
        {
            var orderdetails = db.orderdetails.Include(o => o.menu).Include(o => o.pemesanan);
            return View(orderdetails.ToList());
        }

        // GET: orderdetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orderdetail orderdetail = db.orderdetails.Find(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            return View(orderdetail);
        }

        // GET: orderdetails/Create
        public ActionResult Create()
        {
            ViewBag.menu_id = new SelectList(db.menus, "id", "menu1");
            ViewBag.order_id = new SelectList(db.pemesanans, "id", "status");
            return View();
        }

        // POST: orderdetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nama,jumlah,subtotal,order_id,menu_id")] orderdetail orderdetail)
        {
            if (ModelState.IsValid)
            {
                db.orderdetails.Add(orderdetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.menu_id = new SelectList(db.menus, "id", "menu1", orderdetail.menu_id);
            ViewBag.order_id = new SelectList(db.pemesanans, "id", "status", orderdetail.order_id);
            return View(orderdetail);
        }

        // GET: orderdetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orderdetail orderdetail = db.orderdetails.Find(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.menu_id = new SelectList(db.menus, "id", "menu1", orderdetail.menu_id);
            ViewBag.order_id = new SelectList(db.pemesanans, "id", "status", orderdetail.order_id);
            return View(orderdetail);
        }

        // POST: orderdetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nama,jumlah,subtotal,order_id,menu_id")] orderdetail orderdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.menu_id = new SelectList(db.menus, "id", "menu1", orderdetail.menu_id);
            ViewBag.order_id = new SelectList(db.pemesanans, "id", "status", orderdetail.order_id);
            return View(orderdetail);
        }

        // GET: orderdetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orderdetail orderdetail = db.orderdetails.Find(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            return View(orderdetail);
        }

        // POST: orderdetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            orderdetail orderdetail = db.orderdetails.Find(id);
            db.orderdetails.Remove(orderdetail);
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
