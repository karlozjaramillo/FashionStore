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
    public class SaleController : Controller
    {
        private FashionStoreEntities db = new FashionStoreEntities();

        // GET: Sale
        public ActionResult Index()
        {
            var sale = db.Sale.Include(s => s.Customer).Include(s => s.Product).Include(s => s.Seller);
            return View(sale.ToList());
        }

        // GET: Sale/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sale.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: Sale/Create
        public ActionResult Create()
        {
            ViewBag.idCustomer = new SelectList(db.Customer, "idCustomer", "documentType");
            ViewBag.idProduct = new SelectList(db.Product, "idProduct", "codeProduct");
            ViewBag.idSeller = new SelectList(db.Seller, "idSeller", "names");
            return View();
        }

        // POST: Sale/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSale,idCustomer,idSeller,idProduct,date,total")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Sale.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCustomer = new SelectList(db.Customer, "idCustomer", "documentType", sale.idCustomer);
            ViewBag.idProduct = new SelectList(db.Product, "idProduct", "codeProduct", sale.idProduct);
            ViewBag.idSeller = new SelectList(db.Seller, "idSeller", "names", sale.idSeller);
            return View(sale);
        }

        // GET: Sale/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sale.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCustomer = new SelectList(db.Customer, "idCustomer", "documentType", sale.idCustomer);
            ViewBag.idProduct = new SelectList(db.Product, "idProduct", "codeProduct", sale.idProduct);
            ViewBag.idSeller = new SelectList(db.Seller, "idSeller", "names", sale.idSeller);
            return View(sale);
        }

        // POST: Sale/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idSale,idCustomer,idSeller,idProduct,date,total")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCustomer = new SelectList(db.Customer, "idCustomer", "documentType", sale.idCustomer);
            ViewBag.idProduct = new SelectList(db.Product, "idProduct", "codeProduct", sale.idProduct);
            ViewBag.idSeller = new SelectList(db.Seller, "idSeller", "names", sale.idSeller);
            return View(sale);
        }

        // GET: Sale/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sale.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sale sale = db.Sale.Find(id);
            db.Sale.Remove(sale);
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
