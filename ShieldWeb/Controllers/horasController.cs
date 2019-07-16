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
    public class horasController : Controller
    {
        private SHIELDEntities db = new SHIELDEntities();

        // GET: horas
        public ActionResult Index()
        {
            var hora = db.hora.Include(h => h.AspNetUsers).Include(h => h.comentario).Include(h => h.tarefa);
            return View(hora.ToList());
        }

        // GET: horas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hora hora = db.hora.Find(id);
            if (hora == null)
            {
                return HttpNotFound();
            }
            return View(hora);
        }

        // GET: horas/Create
        public ActionResult Create()
        {
            ViewBag.usuarios_id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.comentarios_id = new SelectList(db.comentario, "id", "descricao");
            ViewBag.tarefas_id = new SelectList(db.tarefa, "id", "titulo");
            return View();
        }

        // POST: horas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,usuarios_id,quantidade_horas,data,tarefas_id,comentarios_id")] hora hora)
        {
            if (ModelState.IsValid)
            {
                db.hora.Add(hora);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.usuarios_id = new SelectList(db.AspNetUsers, "Id", "Email", hora.usuarios_id);
            ViewBag.comentarios_id = new SelectList(db.comentario, "id", "descricao", hora.comentarios_id);
            ViewBag.tarefas_id = new SelectList(db.tarefa, "id", "titulo", hora.tarefas_id);
            return View(hora);
        }

        // GET: horas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hora hora = db.hora.Find(id);
            if (hora == null)
            {
                return HttpNotFound();
            }
            ViewBag.usuarios_id = new SelectList(db.AspNetUsers, "Id", "Email", hora.usuarios_id);
            ViewBag.comentarios_id = new SelectList(db.comentario, "id", "descricao", hora.comentarios_id);
            ViewBag.tarefas_id = new SelectList(db.tarefa, "id", "titulo", hora.tarefas_id);
            return View(hora);
        }

        // POST: horas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,usuarios_id,quantidade_horas,data,tarefas_id,comentarios_id")] hora hora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hora).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.usuarios_id = new SelectList(db.AspNetUsers, "Id", "Email", hora.usuarios_id);
            ViewBag.comentarios_id = new SelectList(db.comentario, "id", "descricao", hora.comentarios_id);
            ViewBag.tarefas_id = new SelectList(db.tarefa, "id", "titulo", hora.tarefas_id);
            return View(hora);
        }

        // GET: horas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hora hora = db.hora.Find(id);
            if (hora == null)
            {
                return HttpNotFound();
            }
            return View(hora);
        }

        // POST: horas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            hora hora = db.hora.Find(id);
            db.hora.Remove(hora);
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
