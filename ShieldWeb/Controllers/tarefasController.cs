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
    public class tarefasController : Controller
    {
        private SHIELDEntities db = new SHIELDEntities();

        // GET: tarefas
        public ActionResult Index()
        {
            var tarefa = db.tarefa.Include(t => t.projeto);
            return View(tarefa.ToList());
        }

        public ActionResult Tasks()
        {
            var task = db.tarefa.Include(s => s.projeto);
            return View(task.ToList());
        }

        // GET: tarefas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tarefa tarefa = db.tarefa.Find(id);
            if (tarefa == null)
            {
                return HttpNotFound();
            }
            return View(tarefa);
        }

        public ActionResult TaskView(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tarefa tarefa = db.tarefa.Find(id);
            if (tarefa == null)
            {
                return HttpNotFound();
            }
            return View(tarefa);
        }

        // GET: tarefas/Create
        public ActionResult Create()
        {
            ViewBag.projetos_id = new SelectList(db.projeto, "id", "nome");
            return View();
        }

        // POST: tarefas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,projetos_id,titulo,tipo_solicitacao,briefing,label,historico_tarefas,avaliacao_criticidade,nivel_criticidade")] tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                db.tarefa.Add(tarefa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.projetos_id = new SelectList(db.projeto, "id", "nome", tarefa.projetos_id);
            return View(tarefa);
        }

        // GET: tarefas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tarefa tarefa = db.tarefa.Find(id);
            if (tarefa == null)
            {
                return HttpNotFound();
            }
            ViewBag.projetos_id = new SelectList(db.projeto, "id", "nome", tarefa.projetos_id);
            return View(tarefa);
        }



        // POST: tarefas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,projetos_id,titulo,tipo_solicitacao,briefing,label,historico_tarefas,avaliacao_criticidade,nivel_criticidade")] tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarefa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.projetos_id = new SelectList(db.projeto, "id", "nome", tarefa.projetos_id);
            return View(tarefa);
        }

        // GET: tarefas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tarefa tarefa = db.tarefa.Find(id);
            if (tarefa == null)
            {
                return HttpNotFound();
            }
            return View(tarefa);
        }

        // POST: tarefas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tarefa tarefa = db.tarefa.Find(id);
            db.tarefa.Remove(tarefa);
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
