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
    public class InstitucionesSaludController : Controller
    {
        private ASOLHEPAEntities db = new ASOLHEPAEntities();

        // GET: InstitucionesSalud
        public ActionResult Index()
        {
            var institucionesSaluds = db.InstitucionesSaluds.Include(i => i.ServiciosEspecializadosInstitucione).Include(i => i.ServiciosEspecializadosInstitucione1);
            return View(institucionesSaluds.OrderBy(i => i.Nombre).ToList());
        }

        // GET: InstitucionesSalud/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstitucionesSalud institucionesSalud = db.InstitucionesSaluds.Find(id);
            if (institucionesSalud == null)
            {
                return HttpNotFound();
            }
            return View(institucionesSalud);
        }

        // GET: InstitucionesSalud/Create
        public ActionResult Create()
        {
            ViewBag.IdServicioEspecializado = new SelectList(db.ServiciosEspecializadosInstituciones, "Id", "Servicio");
            ViewBag.IdServicioEspecializado2 = new SelectList(db.ServiciosEspecializadosInstituciones, "Id", "Servicio");
            return View();
        }

        // POST: InstitucionesSalud/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,IdentificacionNIT,Dirección,Teléfono,Teléfono2,Observaciones,IdServicioEspecializado,IdServicioEspecializado2")] InstitucionesSalud institucionesSalud)
        {
            if (ModelState.IsValid)
            {
                db.InstitucionesSaluds.Add(institucionesSalud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdServicioEspecializado = new SelectList(db.ServiciosEspecializadosInstituciones, "Id", "Servicio", institucionesSalud.IdServicioEspecializado);
            ViewBag.IdServicioEspecializado2 = new SelectList(db.ServiciosEspecializadosInstituciones, "Id", "Servicio", institucionesSalud.IdServicioEspecializado2);
            return View(institucionesSalud);
        }

        // GET: InstitucionesSalud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstitucionesSalud institucionesSalud = db.InstitucionesSaluds.Find(id);
            if (institucionesSalud == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdServicioEspecializado = new SelectList(db.ServiciosEspecializadosInstituciones, "Id", "Servicio", institucionesSalud.IdServicioEspecializado);
            ViewBag.IdServicioEspecializado2 = new SelectList(db.ServiciosEspecializadosInstituciones, "Id", "Servicio", institucionesSalud.IdServicioEspecializado2);
            return View(institucionesSalud);
        }

        // POST: InstitucionesSalud/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,IdentificacionNIT,Dirección,Teléfono,Teléfono2,Observaciones,IdServicioEspecializado,IdServicioEspecializado2")] InstitucionesSalud institucionesSalud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(institucionesSalud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdServicioEspecializado = new SelectList(db.ServiciosEspecializadosInstituciones, "Id", "Servicio", institucionesSalud.IdServicioEspecializado);
            ViewBag.IdServicioEspecializado2 = new SelectList(db.ServiciosEspecializadosInstituciones, "Id", "Servicio", institucionesSalud.IdServicioEspecializado2);
            return View(institucionesSalud);
        }

        // GET: InstitucionesSalud/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstitucionesSalud institucionesSalud = db.InstitucionesSaluds.Find(id);
            if (institucionesSalud == null)
            {
                return HttpNotFound();
            }
            return View(institucionesSalud);
        }

        // POST: InstitucionesSalud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InstitucionesSalud institucionesSalud = db.InstitucionesSaluds.Find(id);
            db.InstitucionesSaluds.Remove(institucionesSalud);
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
