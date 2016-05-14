using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChampionshipWeb.Models;

namespace ChampionshipWeb.Controllers
{
    public class ChampionshipsController : Controller
    {
        private championshipEntities db = new championshipEntities();

        // GET: Championships
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.championships.ToList());
        }

        // GET: Championships/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            championship championship = db.championships.Find(id);
            if (championship == null)
            {
                return HttpNotFound();
            }

            IEnumerable<stage> allStages = null;
            allStages = from stage in db.stages.ToList()
                        where stage.championship_id == id
                         select stage;
            ViewBag.allStages = allStages;

            return View(championship);
        }

        // GET: Championships/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Championships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] championship championship)
        {
            if (ModelState.IsValid)
            {
                db.championships.Add(championship);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(championship);
        }

        // GET: Championships/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            championship championship = db.championships.Find(id);
            if (championship == null)
            {
                return HttpNotFound();
            }
            return View(championship);
        }

        // POST: Championships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] championship championship)
        {
            if (ModelState.IsValid)
            {
                db.Entry(championship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(championship);
        }

        // GET: Championships/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            championship championship = db.championships.Find(id);
            if (championship == null)
            {
                return HttpNotFound();
            }
            return View(championship);
        }

        // POST: Championships/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            championship championship = db.championships.Find(id);
            db.championships.Remove(championship);
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

        private bool isPlayerExist(int? id)
        {
            if (id == null)
                return false;
            championship championship = db.championships.Find(id);
            if (championship == null)
                return false;
            return true;
        }
    }
}
