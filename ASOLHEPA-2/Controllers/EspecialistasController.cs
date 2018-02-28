using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASOLHEPA_2;
using PagedList;

namespace ASOLHEPA_2.Controllers
{
    public class EspecialistasController : Controller
    {
        private ASOLHEPAEntities db = new ASOLHEPAEntities();

        // GET: Especialistas
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NombresSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.IdentificacionSortParm = sortOrder == "Identificacion" ? "identificacion_desc" : "Identificacion";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var especialista = from s in db.Especialistas
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                especialista = especialista.Where(s => s.Nombres.Contains(searchString)
                               || s.Observaciones.Contains(searchString)
                               || s.Identificación.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    especialista = especialista.OrderByDescending(s => s.Nombres);
                    break;
                case "Identificacion":
                    especialista = especialista.OrderBy(s => s.Identificación);
                    break;
                case "identificacion_desc":
                    especialista = especialista.OrderByDescending(s => s.Identificación);
                    break;
                default:  // Name ascending 
                    especialista = especialista.OrderBy(s => s.Nombres);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            // Pasa valor de suma de tareas y total de tareas sumadas
            //especialista = especialista.GroupJoin(especialista,x => x.IdSolicitud, );
            return View(especialista.ToPagedList(pageNumber, pageSize));
        }

        // GET: Especialistas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialista especialista = db.Especialistas.Find(id);
            if (especialista == null)
            {
                return HttpNotFound();
            }
            return View(especialista);
        }

        // GET: Especialistas/Create
        public ActionResult Create()
        {
            ViewBag.IdInstitucionSalud1 = new SelectList(db.InstitucionesSaluds, "Id", "Nombre");
            ViewBag.idInstitucionSalud2 = new SelectList(db.InstitucionesSaluds, "Id", "Nombre");
            ViewBag.idInstitucionSalud3 = new SelectList(db.InstitucionesSaluds, "Id", "Nombre");
            return View();
        }

        // POST: Especialistas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombres,Identificación,Teléfono,CorreoElectrónico,IdInstitucionSalud1,idInstitucionSalud2,idInstitucionSalud3,Observaciones,Especialidad,Dirección")] Especialista especialista)
        {
            if (ModelState.IsValid)
            {
                db.Especialistas.Add(especialista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdInstitucionSalud1 = new SelectList(db.InstitucionesSaluds, "Id", "Nombre", especialista.IdInstitucionSalud1);
            ViewBag.idInstitucionSalud2 = new SelectList(db.InstitucionesSaluds, "Id", "Nombre", especialista.idInstitucionSalud2);
            ViewBag.idInstitucionSalud3 = new SelectList(db.InstitucionesSaluds, "Id", "Nombre", especialista.idInstitucionSalud3);
            return View(especialista);
        }

        // GET: Especialistas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialista especialista = db.Especialistas.Find(id);
            if (especialista == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdInstitucionSalud1 = new SelectList(db.InstitucionesSaluds, "Id", "Nombre", especialista.IdInstitucionSalud1);
            ViewBag.idInstitucionSalud2 = new SelectList(db.InstitucionesSaluds, "Id", "Nombre", especialista.idInstitucionSalud2);
            ViewBag.idInstitucionSalud3 = new SelectList(db.InstitucionesSaluds, "Id", "Nombre", especialista.idInstitucionSalud3);
            return View(especialista);
        }

        // POST: Especialistas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombres,Identificación,Teléfono,CorreoElectrónico,IdInstitucionSalud1,idInstitucionSalud2,idInstitucionSalud3,Observaciones,Especialidad,Dirección")] Especialista especialista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(especialista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdInstitucionSalud1 = new SelectList(db.InstitucionesSaluds, "Id", "Nombre", especialista.IdInstitucionSalud1);
            ViewBag.idInstitucionSalud2 = new SelectList(db.InstitucionesSaluds, "Id", "Nombre", especialista.idInstitucionSalud2);
            ViewBag.idInstitucionSalud3 = new SelectList(db.InstitucionesSaluds, "Id", "Nombre", especialista.idInstitucionSalud3);
            return View(especialista);
        }

        // GET: Especialistas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialista especialista = db.Especialistas.Find(id);
            if (especialista == null)
            {
                return HttpNotFound();
            }
            return View(especialista);
        }

        // POST: Especialistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Especialista especialista = db.Especialistas.Find(id);
            db.Especialistas.Remove(especialista);
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
