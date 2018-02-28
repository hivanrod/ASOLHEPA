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
    public class ServiciosEspecializadosInstitucionesController : Controller
    {
        private ASOLHEPAEntities db = new ASOLHEPAEntities();

        // GET: ServiciosEspecializadosInstituciones
        public ActionResult Index()
        {
            return View(db.ServiciosEspecializadosInstituciones.OrderBy(s => s.Servicio).ToList());
        }

        // GET: ServiciosEspecializadosInstituciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiciosEspecializadosInstitucione serviciosEspecializadosInstitucione = db.ServiciosEspecializadosInstituciones.Find(id);
            if (serviciosEspecializadosInstitucione == null)
            {
                return HttpNotFound();
            }
            return View(serviciosEspecializadosInstitucione);
        }

        // GET: ServiciosEspecializadosInstituciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiciosEspecializadosInstituciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Servicio")] ServiciosEspecializadosInstitucione serviciosEspecializadosInstitucione)
        {
            if (ModelState.IsValid)
            {
                db.ServiciosEspecializadosInstituciones.Add(serviciosEspecializadosInstitucione);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviciosEspecializadosInstitucione);
        }

        // GET: ServiciosEspecializadosInstituciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiciosEspecializadosInstitucione serviciosEspecializadosInstitucione = db.ServiciosEspecializadosInstituciones.Find(id);
            if (serviciosEspecializadosInstitucione == null)
            {
                return HttpNotFound();
            }
            return View(serviciosEspecializadosInstitucione);
        }

        // POST: ServiciosEspecializadosInstituciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Servicio")] ServiciosEspecializadosInstitucione serviciosEspecializadosInstitucione)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviciosEspecializadosInstitucione).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviciosEspecializadosInstitucione);
        }

        // GET: ServiciosEspecializadosInstituciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiciosEspecializadosInstitucione serviciosEspecializadosInstitucione = db.ServiciosEspecializadosInstituciones.Find(id);
            if (serviciosEspecializadosInstitucione == null)
            {
                return HttpNotFound();
            }
            return View(serviciosEspecializadosInstitucione);
        }

        // POST: ServiciosEspecializadosInstituciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiciosEspecializadosInstitucione serviciosEspecializadosInstitucione = db.ServiciosEspecializadosInstituciones.Find(id);
            db.ServiciosEspecializadosInstituciones.Remove(serviciosEspecializadosInstitucione);
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
