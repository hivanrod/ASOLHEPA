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
    public class ProcedimientosController : Controller
    {
        private ASOLHEPAEntities db = new ASOLHEPAEntities();

        // GET: Procedimientos
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NombreSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CodigoSortParm = sortOrder == "Codigo" ? "codigo_desc" : "Codigo";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var procedimiento = from s in db.Procedimientos
                               select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                procedimiento = procedimiento.Where(s => s.Nombre.Contains(searchString)
                               || s.Codigo.Contains(searchString)
                               || s.Descripción.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    procedimiento = procedimiento.OrderByDescending(s => s.Nombre);
                    break;
                case "Codigo":
                    procedimiento = procedimiento.OrderBy(s => s.Codigo);
                    break;
                case "codigo_desc":
                    procedimiento = procedimiento.OrderByDescending(s => s.Codigo);
                    break;
                default:  // Name ascending 
                    procedimiento = procedimiento.OrderBy(s => s.Nombre);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            // Pasa valor de suma de tareas y total de tareas sumadas
            //procedimiento = procedimiento.GroupJoin(procedimiento,x => x.IdSolicitud, );
            return View(procedimiento.ToPagedList(pageNumber, pageSize));
        }

        // GET: Procedimientos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Procedimiento procedimiento = db.Procedimientos.Find(id);
            if (procedimiento == null)
            {
                return HttpNotFound();
            }
            return View(procedimiento);
        }

        // GET: Procedimientos/Create
        public ActionResult Create()
        {
            ViewBag.IdTipoTecnologiaSalud = new SelectList(db.TipoTecnologiaSaluds, "Id", "Descripción");
            return View();
        }

        // POST: Procedimientos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Codigo,Nombre,Descripción,IdAutorizacion,ValorAproximado,ValorReal,ValorAsociación,IdTipoTecnologiaSalud")] Procedimiento procedimiento)
        {
            if (ModelState.IsValid)
            {
                db.Procedimientos.Add(procedimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdTipoTecnologiaSalud = new SelectList(db.TipoTecnologiaSaluds, "Id", "Descripción", procedimiento.IdTipoTecnologiaSalud);
            return View(procedimiento);
        }

        // GET: Procedimientos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Procedimiento procedimiento = db.Procedimientos.Find(id);
            if (procedimiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTipoTecnologiaSalud = new SelectList(db.TipoTecnologiaSaluds, "Id", "Descripción", procedimiento.IdTipoTecnologiaSalud);
            return View(procedimiento);
        }

        // POST: Procedimientos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo,Nombre,Descripción,IdAutorizacion,ValorAproximado,ValorReal,ValorAsociación,IdTipoTecnologiaSalud")] Procedimiento procedimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(procedimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTipoTecnologiaSalud = new SelectList(db.TipoTecnologiaSaluds, "Id", "Descripción", procedimiento.IdTipoTecnologiaSalud);
            return View(procedimiento);
        }

        // GET: Procedimientos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Procedimiento procedimiento = db.Procedimientos.Find(id);
            if (procedimiento == null)
            {
                return HttpNotFound();
            }
            return View(procedimiento);
        }

        // POST: Procedimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Procedimiento procedimiento = db.Procedimientos.Find(id);
            db.Procedimientos.Remove(procedimiento);
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
