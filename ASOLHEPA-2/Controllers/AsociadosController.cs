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
    public class AsociadosController : Controller
    {
        private ASOLHEPAEntities db = new ASOLHEPAEntities();

        // GET: Asociados
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NombresSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CarnetSortParm = sortOrder == "Carnet" ? "carnet_desc" : "Carnet";
            ViewBag.IdentificacionSortParm = sortOrder == "Identificacion" ? "identificacion_desc" : "Identificacion";
            ViewBag.EstadoAsociadoSortParm = sortOrder == "EstadoAsociado" ? "estadoasociado_desc" : "EstadoAsociado";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var asociado = from s in db.Asociados
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                asociado = asociado.Where(s => s.Nombres.Contains(searchString)
                               || s.Observaciones.Contains(searchString)
                               || s.Carnet.Contains(searchString)
                               || s.EstadoAsociado.Estado.Contains(searchString)
                               || s.Identificación.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    asociado = asociado.OrderByDescending(s => s.Nombres);
                    break;
                case "Carnet":
                    asociado = asociado.OrderBy(s => s.Carnet);
                    break;
                case "carnet_desc":
                    asociado = asociado.OrderByDescending(s => s.Carnet);
                    break;
                case "EstadoAsociado":
                    asociado = asociado.OrderBy(s => s.EstadoAsociado.Estado);
                    break;
                case "estadoasociado_desc":
                    asociado = asociado.OrderByDescending(s => s.EstadoAsociado.Estado);
                    break;
                case "Identificacion":
                    asociado = asociado.OrderBy(s => s.Identificación);
                    break;
                case "identificacion_desc":
                    asociado = asociado.OrderByDescending(s => s.Identificación);
                    break;
                default:  // Name ascending 
                    asociado = asociado.OrderBy(s => s.Nombres);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            // Pasa valor de suma de tareas y total de tareas sumadas
            //asociado = asociado.GroupJoin(asociado,x => x.IdSolicitud, );
            return View(asociado.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Menores()
        {

            //Lista los catalogados como menores en los registros
            var asociado = from s in db.Asociados
                           where s.CatalogaMenor == true
                           select s;

            return View(asociado.ToList());

        }
        // GET: Asociados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asociado asociado = db.Asociados.Find(id);
            if (asociado == null)
            {
                return HttpNotFound();
            }
            return View(asociado);
        }

        // GET: Asociados/Create
        public ActionResult Create()
        {
            ViewBag.IdEntidadSalud = new SelectList(db.EntidadesSaluds, "Id", "Nombres");
            ViewBag.IdEspecialista = new SelectList(db.Especialistas, "Id", "Nombres");
            ViewBag.IdEstadoAsociado = new SelectList(db.EstadoAsociadoes, "Id", "Estado");
            ViewBag.IdInstitucion = new SelectList(db.InstitucionesSaluds, "Id", "Nombre");
            ViewBag.IdRegimen = new SelectList(db.RegimenesSaluds, "Id", "Descripción");
            return View();
        }

        // POST: Asociados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombres,Carnet,Dirección,Ciudad,Teléfono,Télefono2,Celular,CorreoElectrónico,Profesión,Telefono3,Acudiente,CatalogaMenor,Identificación,IdEstadoAsociado,LugarNacimiento,FechaNacimiento,FechaInscripción,IdEntidadSalud,IdInstitucion,Patología,TipoPatología,IdEspecialista,Observaciones,FechaRetiro,IdRegimen")] Asociado asociado)
        {
            if (ModelState.IsValid)
            {
                db.Asociados.Add(asociado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEntidadSalud = new SelectList(db.EntidadesSaluds, "Id", "Nombres", asociado.IdEntidadSalud);
            ViewBag.IdEspecialista = new SelectList(db.Especialistas, "Id", "Nombres", asociado.IdEspecialista);
            ViewBag.IdEstadoAsociado = new SelectList(db.EstadoAsociadoes, "Id", "Estado", asociado.IdEstadoAsociado);
            ViewBag.IdInstitucion = new SelectList(db.InstitucionesSaluds, "Id", "Nombre", asociado.IdInstitucion);
            ViewBag.IdRegimen = new SelectList(db.RegimenesSaluds, "Id", "Descripción", asociado.IdRegimen);
            return View(asociado);
        }

        // GET: Asociados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asociado asociado = db.Asociados.Find(id);
            if (asociado == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEntidadSalud = new SelectList(db.EntidadesSaluds, "Id", "Nombres", asociado.IdEntidadSalud);
            ViewBag.IdEspecialista = new SelectList(db.Especialistas, "Id", "Nombres", asociado.IdEspecialista);
            ViewBag.IdEstadoAsociado = new SelectList(db.EstadoAsociadoes, "Id", "Estado", asociado.IdEstadoAsociado);
            ViewBag.IdInstitucion = new SelectList(db.InstitucionesSaluds, "Id", "Nombre", asociado.IdInstitucion);
            ViewBag.IdRegimen = new SelectList(db.RegimenesSaluds, "Id", "Descripción", asociado.IdRegimen);
            return View(asociado);
        }

        // POST: Asociados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombres,Carnet,Dirección,Ciudad,Teléfono,Télefono2,Celular,CorreoElectrónico,Profesión,Telefono3,Acudiente,CatalogaMenor,Identificación,IdEstadoAsociado,LugarNacimiento,FechaNacimiento,FechaInscripción,IdEntidadSalud,IdInstitucion,Patología,TipoPatología,IdEspecialista,Observaciones,FechaRetiro,IdRegimen")] Asociado asociado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asociado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEntidadSalud = new SelectList(db.EntidadesSaluds, "Id", "Nombres", asociado.IdEntidadSalud);
            ViewBag.IdEspecialista = new SelectList(db.Especialistas, "Id", "Nombres", asociado.IdEspecialista);
            ViewBag.IdEstadoAsociado = new SelectList(db.EstadoAsociadoes, "Id", "Estado", asociado.IdEstadoAsociado);
            ViewBag.IdInstitucion = new SelectList(db.InstitucionesSaluds, "Id", "Nombre", asociado.IdInstitucion);
            ViewBag.IdRegimen = new SelectList(db.RegimenesSaluds, "Id", "Descripción", asociado.IdRegimen);
            return View(asociado);
        }

        // GET: Asociados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asociado asociado = db.Asociados.Find(id);
            if (asociado == null)
            {
                return HttpNotFound();
            }
            return View(asociado);
        }

        // POST: Asociados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asociado asociado = db.Asociados.Find(id);
            db.Asociados.Remove(asociado);
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
