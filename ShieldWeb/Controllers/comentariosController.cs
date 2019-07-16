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
    [Authorize]
    public class comentariosController : Controller
    {
        private SHIELDEntities db = new SHIELDEntities();

        // GET: comentarios
        public ActionResult Index()
        {
            var comentario = db.comentario.Include(c => c.AspNetUsers).Include(c => c.tarefa);
            return View(comentario.ToList());
        }

        // GET: comentarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comentario comentario = db.comentario.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // GET: comentarios/Create
        public ActionResult Create()
        {
            ViewBag.usuarios_id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.tarefas_id = new SelectList(db.tarefa, "id", "titulo");
            return View();
        }

        // POST: comentarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,descricao,notificar,usuarios_id,tarefas_id")] comentario comentario)
        {
            if (ModelState.IsValid)
            {
                db.comentario.Add(comentario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.usuarios_id = new SelectList(db.AspNetUsers, "Id", "Email", comentario.usuarios_id);
            ViewBag.tarefas_id = new SelectList(db.tarefa, "id", "titulo", comentario.tarefas_id);
            return View(comentario);
        }

        // GET: comentarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comentario comentario = db.comentario.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            ViewBag.usuarios_id = new SelectList(db.AspNetUsers, "Id", "Email", comentario.usuarios_id);
            ViewBag.tarefas_id = new SelectList(db.tarefa, "id", "titulo", comentario.tarefas_id);
            return View(comentario);
        }

        // POST: comentarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,descricao,notificar,usuarios_id,tarefas_id")] comentario comentario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comentario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.usuarios_id = new SelectList(db.AspNetUsers, "Id", "Email", comentario.usuarios_id);
            ViewBag.tarefas_id = new SelectList(db.tarefa, "id", "titulo", comentario.tarefas_id);
            return View(comentario);
        }

        // GET: comentarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comentario comentario = db.comentario.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // POST: comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            comentario comentario = db.comentario.Find(id);
            db.comentario.Remove(comentario);
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
