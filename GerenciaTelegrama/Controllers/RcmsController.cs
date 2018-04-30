using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GerenciaTelegrama.Models;

namespace GerenciaTelegrama.Controllers
{
    public class RcmsController : Controller
    {
        private TelegramaEntities db = new TelegramaEntities();

        // GET: Rcms
        public ActionResult Index()
        {
            var rcms = db.Rcms.Include(r => r.Telegrama);
            return View(rcms.ToList());
        }

        // GET: Rcms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rcms rcms = db.Rcms.Find(id);
            if (rcms == null)
            {
                return HttpNotFound();
            }
            return View(rcms);
        }

        // GET: Rcms/Create
        public ActionResult Create()
        {
            ViewBag.IdTelegrama = new SelectList(db.Telegrama, "IdTelegrama", "NomeProjeto");
            return View();
        }

        // POST: Rcms/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rcms rcms)
        {
            if (ModelState.IsValid)
            {
                db.Rcms.Add(rcms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdTelegrama = new SelectList(db.Telegrama, "IdTelegrama", "NomeProjeto", rcms.IdTelegrama);
            return View(rcms);
        }

        // GET: Rcms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rcms rcms = db.Rcms.Find(id);
            if (rcms == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTelegrama = new SelectList(db.Telegrama, "IdTelegrama", "NomeProjeto", rcms.IdTelegrama);
            return View(rcms);
        }

        // POST: Rcms/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Rcms rcms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rcms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTelegrama = new SelectList(db.Telegrama, "IdTelegrama", "NomeProjeto", rcms.IdTelegrama);
            return View(rcms);
        }

        // GET: Rcms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rcms rcms = db.Rcms.Find(id);
            if (rcms == null)
            {
                return HttpNotFound();
            }
            return View(rcms);
        }

        // POST: Rcms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rcms rcms = db.Rcms.Find(id);
            db.Rcms.Remove(rcms);
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
