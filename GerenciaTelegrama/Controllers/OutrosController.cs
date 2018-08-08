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
    public class OutrosController : Controller
    {
        private TelegramaEntities db = new TelegramaEntities();


        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, String filtro)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (!String.IsNullOrEmpty(filtro))
            {
                return View(db.Outros.Include(r => r.Telegrama).Where(r => r.Telegrama.NomeProjeto.ToLower().Contains(filtro.ToLower())).ToList());
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

            var telegramas = from s in db.Outros
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                telegramas = telegramas.Where(s => s.CodOutros.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    telegramas = telegramas.OrderByDescending(s => s.CodOutros);
                    break;
                default:  // Name ascending 
                    telegramas = telegramas.OrderBy(s => s.CodOutros);
                    break;
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(telegramas.ToPagedList(pageNumber, pageSize));
        }




        //// GET: Outros
        //public ActionResult Index(String filtro)
        //{

        //    if (!String.IsNullOrEmpty(filtro))
        //    {
        //        return View(db.Outros.Include(r => r.Telegrama).Where(r => r.Telegrama.NomeProjeto.ToLower().Contains(filtro.ToLower())).ToList());
        //    }
        //    //var outros = db.Outros.Include(o => o.Telegrama);

        //    return View(db.Outros.Include(r => r.Telegrama).ToList());
        //    //return View(outros.ToList());
        //}

        // GET: Outros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outros outros = db.Outros.Find(id);
            if (outros == null)
            {
                return HttpNotFound();
            }
            return View(outros);
        }

        // GET: Outros/Create
        public ActionResult Create()
        {
            ViewBag.IdTelegrama = new SelectList(db.Telegrama, "IdTelegrama", "NomeProjeto");
            return View();
        }

        // POST: Outros/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdOutros,Descricao,Data,Valor,IdTelegrama,CodOutros")] Outros outros)
        {
            if (ModelState.IsValid)
            {
                db.Outros.Add(outros);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdTelegrama = new SelectList(db.Telegrama, "IdTelegrama", "NomeProjeto", outros.IdTelegrama);
            return View(outros);
        }

        // GET: Outros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outros outros = db.Outros.Find(id);
            if (outros == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTelegrama = new SelectList(db.Telegrama, "IdTelegrama", "NomeProjeto", outros.IdTelegrama);
            return View(outros);
        }

        // POST: Outros/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOutros,Descricao,Data,Valor,IdTelegrama,CodOutros")] Outros outros)
        {
            if (ModelState.IsValid)
            {
                db.Entry(outros).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTelegrama = new SelectList(db.Telegrama, "IdTelegrama", "NomeProjeto", outros.IdTelegrama);
            return View(outros);
        }

        // GET: Outros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outros outros = db.Outros.Find(id);
            if (outros == null)
            {
                return HttpNotFound();
            }
            return View(outros);
        }

        // POST: Outros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Outros outros = db.Outros.Find(id);
            db.Outros.Remove(outros);
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
