using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FashionStore.Models;

namespace FashionStore.Controllers
{
    public class ReserveController : Controller
    {
        private FashionStoreEntities db = new FashionStoreEntities();

        // GET: Reserve
        public ActionResult Index()
        {
            var reserve = db.Reserve.Include(r => r.Customer).Include(r => r.Product);
            return View(reserve.ToList());
        }

        // GET: Reserve/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserve reserve = db.Reserve.Find(id);
            if (reserve == null)
            {
                return HttpNotFound();
            }
            return View(reserve);
        }

        // GET: Reserve/Create
        public ActionResult Create()
        {
            ViewBag.idCustomer = new SelectList(db.Customer, "idCustomer", "documentType");
            ViewBag.idProduct = new SelectList(db.Product, "idProduct", "codeProduct");
            return View();
        }

        // POST: Reserve/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idReserve,idCustomer,idProduct,date")] Reserve reserve)
        {
            if (ModelState.IsValid)
            {
                db.Reserve.Add(reserve);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCustomer = new SelectList(db.Customer, "idCustomer", "documentType", reserve.idCustomer);
            ViewBag.idProduct = new SelectList(db.Product, "idProduct", "codeProduct", reserve.idProduct);
            return View(reserve);
        }

        // GET: Reserve/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserve reserve = db.Reserve.Find(id);
            if (reserve == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCustomer = new SelectList(db.Customer, "idCustomer", "documentType", reserve.idCustomer);
            ViewBag.idProduct = new SelectList(db.Product, "idProduct", "codeProduct", reserve.idProduct);
            return View(reserve);
        }

        // POST: Reserve/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idReserve,idCustomer,idProduct,date")] Reserve reserve)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reserve).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCustomer = new SelectList(db.Customer, "idCustomer", "documentType", reserve.idCustomer);
            ViewBag.idProduct = new SelectList(db.Product, "idProduct", "codeProduct", reserve.idProduct);
            return View(reserve);
        }

        // GET: Reserve/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserve reserve = db.Reserve.Find(id);
            if (reserve == null)
            {
                return HttpNotFound();
            }
            return View(reserve);
        }

        // POST: Reserve/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reserve reserve = db.Reserve.Find(id);
            db.Reserve.Remove(reserve);
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
