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
    public class EntidadesSaludController : Controller
    {
        private ASOLHEPAEntities db = new ASOLHEPAEntities();

        // GET: EntidadesSalud
        public ActionResult Index()
        {
            var entidadesSalud = from e in db.EntidadesSaluds
                            select e;
            entidadesSalud = entidadesSalud.OrderBy(e => e.Nombres);

            return View(entidadesSalud.ToList());
        }

        // GET: EntidadesSalud/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntidadesSalud entidadesSalud = db.EntidadesSaluds.Find(id);
            if (entidadesSalud == null)
            {
                return HttpNotFound();
            }
            return View(entidadesSalud);
        }

        // GET: EntidadesSalud/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EntidadesSalud/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombres,Identificación,Teléfono,Teléfono2,Observaciones")] EntidadesSalud entidadesSalud)
        {
            if (ModelState.IsValid)
            {
                db.EntidadesSaluds.Add(entidadesSalud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(entidadesSalud);
        }

        // GET: EntidadesSalud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntidadesSalud entidadesSalud = db.EntidadesSaluds.Find(id);
            if (entidadesSalud == null)
            {
                return HttpNotFound();
            }
            return View(entidadesSalud);
        }

        // POST: EntidadesSalud/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombres,Identificación,Teléfono,Teléfono2,Observaciones")] EntidadesSalud entidadesSalud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entidadesSalud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entidadesSalud);
        }

        // GET: EntidadesSalud/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntidadesSalud entidadesSalud = db.EntidadesSaluds.Find(id);
            if (entidadesSalud == null)
            {
                return HttpNotFound();
            }
            return View(entidadesSalud);
        }

        // POST: EntidadesSalud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EntidadesSalud entidadesSalud = db.EntidadesSaluds.Find(id);
            db.EntidadesSaluds.Remove(entidadesSalud);
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
