using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShieldWeb.Models;

namespace ShieldWeb.Controllers
{
    public class projetosController : Controller
    {
        private SHIELDEntities db = new SHIELDEntities();

        // GET: projetoes
        public ActionResult Index()
        {
            var projeto = db.projeto.Include(p => p.cliente);
            return View(projeto.ToList());
        }

        // GET: projetoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            projeto projeto = db.projeto.Find(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(projeto);
        }

        // GET: projetoes/Create
        public ActionResult Create()
        {
            ViewBag.clientes_id = new SelectList(db.cliente, "id", "nome");
            return View();
        }

        // POST: projetoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nome,clientes_id")] projeto projeto)
        {
            if (ModelState.IsValid)
            {
                db.projeto.Add(projeto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.clientes_id = new SelectList(db.cliente, "id", "nome", projeto.clientes_id);
            return View(projeto);
        }

        // GET: projetoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            projeto projeto = db.projeto.Find(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            ViewBag.clientes_id = new SelectList(db.cliente, "id", "nome", projeto.clientes_id);
            return View(projeto);
        }

        // POST: projetoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nome,clientes_id")] projeto projeto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projeto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clientes_id = new SelectList(db.cliente, "id", "nome", projeto.clientes_id);
            return View(projeto);
        }

        // GET: projetoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            projeto projeto = db.projeto.Find(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(projeto);
        }

        // POST: projetoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            projeto projeto = db.projeto.Find(id);
            db.projeto.Remove(projeto);
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
