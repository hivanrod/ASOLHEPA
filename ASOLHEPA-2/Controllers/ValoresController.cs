using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASOLHEPA_2;

namespace ASOLHEPA_2.Controllers
{
    public class ValoresController : Controller
    {
        private ASOLHEPAEntities db = new ASOLHEPAEntities();

        // GET: Valores
        public ActionResult Index()
        {
            var valores = db.Valores.Include(v => v.Asociado);
            return View(valores.ToList());
        }

        // GET: Valores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Valore valore = db.Valores.Find(id);
            if (valore == null)
            {
                return HttpNotFound();
            }
            return View(valore);
        }

        // GET: Valores/Create
        public ActionResult Create()
        {
            ViewBag.IdAsociado = new SelectList(db.Asociados, "Id", "Nombres");
            return View();
        }

        // POST: Valores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdAsociado,Fecha,IdAutorización,IdDonación,OtrosValores,TotalCobro,Observaciones")] Valore valore)
        {
            if (ModelState.IsValid)
            {
                db.Valores.Add(valore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAsociado = new SelectList(db.Asociados, "Id", "Nombres", valore.IdAsociado);
            return View(valore);
        }

        // GET: Valores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Valore valore = db.Valores.Find(id);
            if (valore == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAsociado = new SelectList(db.Asociados, "Id", "Nombres", valore.IdAsociado);
            return View(valore);
        }

        // POST: Valores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdAsociado,Fecha,IdAutorización,IdDonación,OtrosValores,TotalCobro,Observaciones")] Valore valore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(valore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAsociado = new SelectList(db.Asociados, "Id", "Nombres", valore.IdAsociado);
            return View(valore);
        }

        // GET: Valores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Valore valore = db.Valores.Find(id);
            if (valore == null)
            {
                return HttpNotFound();
            }
            return View(valore);
        }

        // POST: Valores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Valore valore = db.Valores.Find(id);
            db.Valores.Remove(valore);
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
