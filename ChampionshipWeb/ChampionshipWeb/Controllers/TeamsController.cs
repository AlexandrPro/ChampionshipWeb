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
    public class TeamsController : Controller
    {
        private championshipEntities db = new championshipEntities();

        // GET: Teams
        [AllowAnonymous]
        public ActionResult Index()
        {
            var teams = db.teams.Include(t => t.city).Include(t => t.coach);
            return View(teams.ToList());
        }

        // GET: Teams/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            team team = db.teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }

            IEnumerable<player> allPlayers = null;
            allPlayers = from player in db.players.Include(p => p.player_state).Include(p => p.position).Include(p => p.team)
                         where player.team_id == id
                         select player;
            ViewBag.allPlayers = allPlayers;

            return View(team);
        }

        // GET: Teams/Create
        [Authorize(Roles = "admin, moder")]
        public ActionResult Create()
        {
            ViewBag.citys_id = new SelectList(db.citys, "id", "name_ru");
            ViewBag.coach_id = new SelectList(db.coaches, "id", "name");
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin, moder")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,short_name,name,coach_id,citys_id")] team team)
        {
            if (ModelState.IsValid)
            {
                db.teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.citys_id = new SelectList(db.citys, "id", "name_ru", team.citys_id);
            ViewBag.coach_id = new SelectList(db.coaches, "id", "name", team.coach_id);
            return View(team);
        }

        // GET: Teams/Edit/5
        [Authorize(Roles = "admin, moder")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            team team = db.teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            ViewBag.citys_id = new SelectList(db.citys, "id", "name_ru", team.citys_id);
            ViewBag.coach_id = new SelectList(db.coaches, "id", "name", team.coach_id);
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin, moder")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,short_name,name,coach_id,citys_id")] team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.citys_id = new SelectList(db.citys, "id", "name_ru", team.citys_id);
            ViewBag.coach_id = new SelectList(db.coaches, "id", "name", team.coach_id);
            return View(team);
        }

        // GET: Teams/Delete/5
        [Authorize(Roles = "admin, moder")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            team team = db.teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin, moder")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            team team = db.teams.Find(id);
            db.teams.Remove(team);
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
