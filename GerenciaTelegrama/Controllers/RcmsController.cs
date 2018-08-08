using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GerenciaTelegrama.Models;
using PagedList;

namespace GerenciaTelegrama.Controllers
{
    public class RcmsController : Controller
    {
        private TelegramaEntities db = new TelegramaEntities();


        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, String filtro)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (!String.IsNullOrEmpty(filtro))
            {
                return View(db.Rcms.Include(r => r.Telegrama).Where(r => r.Telegrama.NomeProjeto.ToLower().Contains(filtro.ToLower())).ToList());
            }


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var telegramas = from s in db.Rcms
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                telegramas = telegramas.Where(s => s.CodRcms.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    telegramas = telegramas.OrderByDescending(s => s.CodRcms);
                    break;
                default:  // Name ascending 
                    telegramas = telegramas.OrderBy(s => s.CodRcms);
                    break;
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(telegramas.ToPagedList(pageNumber, pageSize));
        }



        //// GET: Rcms
        //public ActionResult Index(String filtro)
        //{

        //    if (!String.IsNullOrEmpty(filtro))
        //    {
        //        return View(db.Rcms.Include(r => r.Telegrama).Where(r => r.Telegrama.NomeProjeto.ToLower().Contains(filtro.ToLower())).ToList());
        //    }
        //    return View(db.Rcms.Include(r => r.Telegrama).ToList());
        //}

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
