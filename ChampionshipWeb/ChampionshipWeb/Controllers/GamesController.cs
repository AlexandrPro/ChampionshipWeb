using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ChampionshipWeb.Models;

namespace ChampionshipWeb.Controllers
{
    public class GamesController : Controller
    {
        private championshipEntities db = new championshipEntities();

        // GET: Games
        public ActionResult Index()
        {
            var games = db.games.Include(g => g.game_state)
                .Include(g => g.stage)
                .Include(g => g.team)
                .Include(g => g.team1)
                .OrderByDescending(g => g.date);
            return View(games.ToList());
        }

        [AllowAnonymous]
        public ActionResult PartialIndex()
        {
            return PartialView();
        }

        

        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            game game = db.games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            ViewBag.game_state_id = new SelectList(db.game_state, "id", "name_ru");
            ViewBag.stage_id = new SelectList(db.stages, "id", "name");
            ViewBag.team1_id = new SelectList(db.teams, "id", "short_name");
            ViewBag.team2_id = new SelectList(db.teams, "id", "short_name");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,date,team1_id,team2_id,team1_score,team2_score,place,game_state_id,stage_id")] game game)
        {

            if (ModelState.IsValid && isTeamsInState(game))
            {
                db.games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.game_state_id = new SelectList(db.game_state, "id", "name_ru", game.game_state_id);
            ViewBag.stage_id = new SelectList(db.stages, "id", "name", game.stage_id);
            ViewBag.team1_id = new SelectList(db.teams, "id", "short_name", game.team1_id);
            ViewBag.team2_id = new SelectList(db.teams, "id", "short_name", game.team2_id);
            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            game game = db.games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.game_state_id = new SelectList(db.game_state, "id", "name_ru", game.game_state_id);
            ViewBag.stage_id = new SelectList(db.stages, "id", "name", game.stage_id);
            ViewBag.team1_id = new SelectList(db.teams, "id", "short_name", game.team1_id);
            ViewBag.team2_id = new SelectList(db.teams, "id", "short_name", game.team2_id);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,date,team1_id,team2_id,team1_score,team2_score,place,game_state_id,stage_id")] game game)
        {
            if (ModelState.IsValid && isTeamsInState(game))
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.game_state_id = new SelectList(db.game_state, "id", "name_ru", game.game_state_id);
            ViewBag.stage_id = new SelectList(db.stages, "id", "name", game.stage_id);
            ViewBag.team1_id = new SelectList(db.teams, "id", "short_name", game.team1_id);
            ViewBag.team2_id = new SelectList(db.teams, "id", "short_name", game.team2_id);
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            game game = db.games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            game game = db.games.Find(id);
            db.games.Remove(game);
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

        private bool isTeamsInState(game game)
        {
            int? team1 = game.team1_id;
            int? team2 = game.team2_id;
            int stage = game.stage_id;
            
            IEnumerable<int> teamsInStage = from teams_in_stage in db.teams_in_stage
                               where teams_in_stage.stage_id == stage
                               select teams_in_stage.teams_id;

            bool isTeam1InStage = false;
            bool isTeam2InStage = false;

            foreach (int item in teamsInStage)
            {
                if (team1 == item) isTeam1InStage = true;
                if (team2 == item) isTeam2InStage = true;
            }

            if (isTeam1InStage && isTeam2InStage) return true;
            else return false;
        }
    }
}
