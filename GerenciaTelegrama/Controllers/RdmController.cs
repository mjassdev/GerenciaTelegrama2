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
    public class RdmController : Controller
    {
        private readonly TelegramaEntities _db = new TelegramaEntities();


        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, String filtro)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (!String.IsNullOrEmpty(filtro))
            {
                return View(_db.Rdm.Include(r => r.Telegrama).Where(r => r.Telegrama.NomeProjeto.ToLower().Contains(filtro.ToLower())).ToList());
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

            var telegramas = from s in _db.Rdm
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                telegramas = telegramas.Where(s => s.CodRdm.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    telegramas = telegramas.OrderByDescending(s => s.CodRdm);
                    break;
                default:  // Name ascending 
                    telegramas = telegramas.OrderBy(s => s.CodRdm);
                    break;
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(telegramas.ToPagedList(pageNumber, pageSize));
        }




        //// GET: Rdm
        //public ActionResult Index(String filtro)
        //{

        //    //Permite o filtro pelo nome do projeto
        //    if (!String.IsNullOrEmpty(filtro))
        //    {
        //        return View(_db.Rdm.Include(r => r.Telegrama).Where(r => r.Telegrama.NomeProjeto.ToLower().Contains(filtro.ToLower())).ToList());
        //    }
        //    return View(_db.Rdm.Include(r => r.Telegrama).ToList());


        //   // var rdm = _db.Rdm.Include(r => r.Telegrama);
        //   // return View(rdm.ToList());
        //}

        // GET: Rdm/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rdm rdm = _db.Rdm.Find(id);
            if (rdm == null)
            {
                return HttpNotFound();
            }
            return View(rdm);
        }

        // GET: Rdm/Create
        public ActionResult Create()
        {
            ViewBag.IdTelegrama = new SelectList(_db.Telegrama, "IdTelegrama", "NomeProjeto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rdm rdm)
        {
            if (ModelState.IsValid)
            {
                _db.Rdm.Add(rdm);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdTelegrama = new SelectList(_db.Telegrama, "IdTelegrama", "NomeProjeto", rdm.IdTelegrama);
            return View(rdm);
        }

        // GET: Rdm/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rdm rdm = _db.Rdm.Find(id);
            if (rdm == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTelegrama = new SelectList(_db.Telegrama, "IdTelegrama", "NomeProjeto", rdm.IdTelegrama);
            return View(rdm);
        }

        // POST: Rdm/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Rdm rdm)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(rdm).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTelegrama = new SelectList(_db.Telegrama, "IdTelegrama", "NomeProjeto", rdm.IdTelegrama);
            return View(rdm);
        }

        // GET: Rdm/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rdm rdm = _db.Rdm.Find(id);
            if (rdm == null)
            {
                return HttpNotFound();
            }
            return View(rdm);
        }

        // POST: Rdm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rdm rdm = _db.Rdm.Find(id);
            _db.Rdm.Remove(rdm);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
