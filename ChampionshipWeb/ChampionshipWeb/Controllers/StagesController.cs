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
    public class StagesController : Controller
    {
        private championshipEntities db = new championshipEntities();

        // GET: Stages
        [AllowAnonymous]
        public ActionResult Index()
        {
            var stages = db.stages.Include(s => s.championship).Include(s => s.type_of_stage);
            return View(stages.ToList());
        }

        [AllowAnonymous]
        public ActionResult PartialIndex()
        {
            return PartialView();
        }
        

        // GET: Stages/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stage stage = db.stages.Find(id);
            if (stage == null)
            {
                return HttpNotFound();
            }

            IEnumerable<game> allGames = null;
            allGames = from game in db.games
                       .Include(g => g.game_state)
                       .Include(g => g.stage)
                       .Include(g => g.team)
                       .Include(g => g.team1)
                       .OrderByDescending(g => g.date)
                       where game.stage_id == id
                       select game;
            ViewBag.allGames = allGames;


            return View(stage);
        }

        [AllowAnonymous]
        public ActionResult GroupTable(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stage stage = db.stages.Find(id);
            if (stage == null)
            {
                return HttpNotFound();
            }

            IEnumerable<game> allGames = null;
            allGames = from game in db.games
                       .Include(g => g.game_state)
                       .Include(g => g.stage)
                       .Include(g => g.team)
                       .Include(g => g.team1)
                       where game.stage_id == id
                       select game;

            IEnumerable<teams_in_stage> allTeamsInStage = null;
            allTeamsInStage = from teams_in_stage in db.teams_in_stage
                              where teams_in_stage.stage_id == id
                              select teams_in_stage;
            int[] ArrTeamsInStage = new int[allTeamsInStage.Count()];
            for(int i = 0; i < ArrTeamsInStage.Count(); i++)
            {
                ArrTeamsInStage[i] = allTeamsInStage.ElementAt(i).teams_id;
            }
            ViewBag.TeamsInStage = ArrTeamsInStage;

            ViewBag.allGames = allGames;
            return View(stage);
        }

        // GET: Stages/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.championship_id = new SelectList(db.championships, "id", "name");
            ViewBag.type_of_stage_id = new SelectList(db.type_of_stage, "id", "name_ru");
            return View();
        }

        // POST: Stages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,championship_id,type_of_stage_id")] stage stage)
        {
            if (ModelState.IsValid)
            {
                db.stages.Add(stage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.championship_id = new SelectList(db.championships, "id", "name", stage.championship_id);
            ViewBag.type_of_stage_id = new SelectList(db.type_of_stage, "id", "name_ru", stage.type_of_stage_id);
            return View(stage);
        }

        // GET: Stages/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stage stage = db.stages.Find(id);
            if (stage == null)
            {
                return HttpNotFound();
            }
            ViewBag.championship_id = new SelectList(db.championships, "id", "name", stage.championship_id);
            ViewBag.type_of_stage_id = new SelectList(db.type_of_stage, "id", "name_ru", stage.type_of_stage_id);
            return View(stage);
        }

        // POST: Stages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,championship_id,type_of_stage_id")] stage stage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.championship_id = new SelectList(db.championships, "id", "name", stage.championship_id);
            ViewBag.type_of_stage_id = new SelectList(db.type_of_stage, "id", "name_ru", stage.type_of_stage_id);
            return View(stage);
        }

        // GET: Stages/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stage stage = db.stages.Find(id);
            if (stage == null)
            {
                return HttpNotFound();
            }
            return View(stage);
        }

        // POST: Stages/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            stage stage = db.stages.Find(id);
            db.stages.Remove(stage);
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
