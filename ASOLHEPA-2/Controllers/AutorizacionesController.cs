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
    public class AutorizacionesController : Controller
    {
        private ASOLHEPAEntities db = new ASOLHEPAEntities();

        // GET: Autorizaciones
        public ActionResult Index()
        {
            var autorizaciones = db.Autorizaciones.Include(a => a.Asociado).Include(a => a.EntidadesSalud).Include(a => a.InstitucionesSalud).Include(a => a.Procedimiento);
            return View(autorizaciones.ToList());
        }

        // GET: Autorizaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autorizacione autorizacione = db.Autorizaciones.Find(id);
            if (autorizacione == null)
            {
                return HttpNotFound();
            }
            return View(autorizacione);
        }

        // GET: Autorizaciones/Create
        public ActionResult Create()
        {
            ViewBag.IdAsociado = new SelectList(db.Asociados, "Id", "Nombres");
            ViewBag.IdEntidad = new SelectList(db.EntidadesSaluds, "Id", "Nombres");
            ViewBag.IdInstitución = new SelectList(db.InstitucionesSaluds, "Id", "Nombre");
            ViewBag.IdProcedimiento = new SelectList(db.Procedimientos, "Id", "Codigo");
            return View();
        }

        // POST: Autorizaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdEntidad,IdInstitución,IdProcedimiento,IdAsociado,Fecha,FechaAutorización,Autoriza,FechaPrescripción,FechaDisfruteDerecho")] Autorizacione autorizacione)
        {
            if (ModelState.IsValid)
            {
                db.Autorizaciones.Add(autorizacione);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAsociado = new SelectList(db.Asociados, "Id", "Nombres", autorizacione.IdAsociado);
            ViewBag.IdEntidad = new SelectList(db.EntidadesSaluds, "Id", "Nombres", autorizacione.IdEntidad);
            ViewBag.IdInstitución = new SelectList(db.InstitucionesSaluds, "Id", "Nombre", autorizacione.IdInstitución);
            ViewBag.IdProcedimiento = new SelectList(db.Procedimientos, "Id", "Codigo", autorizacione.IdProcedimiento);
            return View(autorizacione);
        }

        // GET: Autorizaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autorizacione autorizacione = db.Autorizaciones.Find(id);
            if (autorizacione == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAsociado = new SelectList(db.Asociados, "Id", "Nombres", autorizacione.IdAsociado);
            ViewBag.IdEntidad = new SelectList(db.EntidadesSaluds, "Id", "Nombres", autorizacione.IdEntidad);
            ViewBag.IdInstitución = new SelectList(db.InstitucionesSaluds, "Id", "Nombre", autorizacione.IdInstitución);
            ViewBag.IdProcedimiento = new SelectList(db.Procedimientos, "Id", "Codigo", autorizacione.IdProcedimiento);
            return View(autorizacione);
        }

        // POST: Autorizaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdEntidad,IdInstitución,IdProcedimiento,IdAsociado,Fecha,FechaAutorización,Autoriza,FechaPrescripción,FechaDisfruteDerecho")] Autorizacione autorizacione)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autorizacione).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAsociado = new SelectList(db.Asociados, "Id", "Nombres", autorizacione.IdAsociado);
            ViewBag.IdEntidad = new SelectList(db.EntidadesSaluds, "Id", "Nombres", autorizacione.IdEntidad);
            ViewBag.IdInstitución = new SelectList(db.InstitucionesSaluds, "Id", "Nombre", autorizacione.IdInstitución);
            ViewBag.IdProcedimiento = new SelectList(db.Procedimientos, "Id", "Codigo", autorizacione.IdProcedimiento);
            return View(autorizacione);
        }

        // GET: Autorizaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autorizacione autorizacione = db.Autorizaciones.Find(id);
            if (autorizacione == null)
            {
                return HttpNotFound();
            }
            return View(autorizacione);
        }

        // POST: Autorizaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Autorizacione autorizacione = db.Autorizaciones.Find(id);
            db.Autorizaciones.Remove(autorizacione);
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
