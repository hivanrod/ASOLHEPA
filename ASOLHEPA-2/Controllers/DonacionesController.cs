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
    public class DonacionesController : Controller
    {
        private ASOLHEPAEntities db = new ASOLHEPAEntities();

        // GET: Donaciones
        public ActionResult Index()
        {
            var donaciones = db.Donaciones.Include(d => d.Asociado);
            return View(donaciones.ToList());
        }

        // GET: Donaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donacione donacione = db.Donaciones.Find(id);
            if (donacione == null)
            {
                return HttpNotFound();
            }
            return View(donacione);
        }

        // GET: Donaciones/Create
        public ActionResult Create()
        {
            ViewBag.IdAsociado = new SelectList(db.Asociados.OrderBy(a => a.Nombres), "Id", "Nombres");
            return View();
        }

        // POST: Donaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdAsociado,Fecha,Valor,Obsevaciones")] Donacione donacione)
        {
            if (ModelState.IsValid)
            {
                db.Donaciones.Add(donacione);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAsociado = new SelectList(db.Asociados, "Id", "Nombres", donacione.IdAsociado);
            return View(donacione);
        }

        // GET: Donaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donacione donacione = db.Donaciones.Find(id);
            if (donacione == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAsociado = new SelectList(db.Asociados, "Id", "Nombres", donacione.IdAsociado);
            return View(donacione);
        }

        // POST: Donaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdAsociado,Fecha,Valor,Obsevaciones")] Donacione donacione)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donacione).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAsociado = new SelectList(db.Asociados, "Id", "Nombres", donacione.IdAsociado);
            return View(donacione);
        }

        // GET: Donaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donacione donacione = db.Donaciones.Find(id);
            if (donacione == null)
            {
                return HttpNotFound();
            }
            return View(donacione);
        }

        // POST: Donaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donacione donacione = db.Donaciones.Find(id);
            db.Donaciones.Remove(donacione);
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
