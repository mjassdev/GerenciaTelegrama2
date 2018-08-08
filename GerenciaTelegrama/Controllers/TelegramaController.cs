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
    public class TelegramaController : Controller
    {
        private TelegramaEntities db = new TelegramaEntities();


        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var telegramas = from s in db.Telegrama
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                telegramas = telegramas.Where(s => s.NomeProjeto.Contains(searchString)
                                       || s.CodTelegrama.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    telegramas = telegramas.OrderByDescending(s => s.NomeProjeto);
                    break;
                default:  // Name ascending 
                    telegramas = telegramas.OrderBy(s => s.NomeProjeto);
                    break;
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(telegramas.ToPagedList(pageNumber, pageSize));
        }



        //// GET: Telegrama
        //public ActionResult Index()
        //{
        //    return View(db.Telegrama.ToList());
        //}


        //ACTION MODAL
        public ActionResult BootstrapDialog()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Lyubomir()
        {
            return RedirectToAction("Index");
        }


        // GET: Telegrama/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telegrama telegrama = db.Telegrama.Find(id);
            if (telegrama == null)
            {
                return HttpNotFound();
            }
            return View(telegrama);
        }

        // GET: Telegrama/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Telegrama telegrama)
        {   
            if (ModelState.IsValid)
            {
                db.Telegrama.Add(telegrama);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(telegrama);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var telegrama = db.Telegrama.Find(id);
            if (telegrama == null)
            {
                return HttpNotFound();
            }
            return View(telegrama);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Telegrama telegrama)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telegrama).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(telegrama);
        }

        // GET: Telegrama/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telegrama telegrama = db.Telegrama.Find(id);
            if (telegrama == null)
            {
                return HttpNotFound();
            }
            return View(telegrama);
        }





        // POST: Telegrama/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Telegrama telegrama = db.Telegrama.Find(id);
            db.Telegrama.Remove(telegrama);
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
