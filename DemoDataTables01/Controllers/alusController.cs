using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DemoDataTables01.Models;

namespace DemoDataTables01.Controllers
{
    public class alusController : Controller
    {
        private BDModels db = new BDModels();

        // GET: alus
        public ActionResult Index()
        {
            return View(db.alu.ToList());
        }

        // GET: alus/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            alu alu = db.alu.Find(id);
            if (alu == null)
            {
                return HttpNotFound();
            }
            return View(alu);
        }


        // GET: alus/Detalle/5
        public ActionResult Detalle(string id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("Index", "alus");
            }
            alu alu = db.alu.Find(id);
            if (alu == null)
            {
                return HttpNotFound();
            }
            return View(alu);
        }





        // GET: alus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: alus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "carnet,apellido1,apellido2,apellido3,nombre1,nombre2,cod_carr,activo,curso_asp,proceso,fecha_pro,fecha_nac,dui,nit,recibo_ins,ciclo_in,año_in,cod_carr_old,carnet_old,equi,sexo")] alu alu)
        {
            if (ModelState.IsValid)
            {
                db.alu.Add(alu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(alu);
        }

        // GET: alus/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            alu alu = db.alu.Find(id);
            if (alu == null)
            {
                return HttpNotFound();
            }
            return View(alu);
        }

        // POST: alus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "carnet,apellido1,apellido2,apellido3,nombre1,nombre2,cod_carr,activo,curso_asp,proceso,fecha_pro,fecha_nac,dui,nit,recibo_ins,ciclo_in,año_in,cod_carr_old,carnet_old,equi,sexo")] alu alu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(alu);
        }

        // GET: alus/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            alu alu = db.alu.Find(id);
            if (alu == null)
            {
                return HttpNotFound();
            }
            return View(alu);
        }

        // POST: alus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            alu alu = db.alu.Find(id);
            db.alu.Remove(alu);
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
