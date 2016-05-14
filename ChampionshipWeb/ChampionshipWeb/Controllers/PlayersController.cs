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
    public class PlayersController : Controller
    {
        private championshipEntities db = new championshipEntities();

        // GET: Players
        [AllowAnonymous]
        public ActionResult Index()
        {
            var players = db.players.Include(p => p.player_state).Include(p => p.position).Include(p => p.team);

            //добавляем поля для фильтрации
            List<team> Teams = db.teams.ToList();
            //Добавляем в список возможность выбора всех
            Teams.Insert(0, new team { short_name = "Все", id = 0 });
            ViewBag.Teams = new SelectList(Teams, "id", "short_name");

            return View(players.ToList());
        }

        //Поиск игроков по команде и 
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(int team)
        {
            IEnumerable<player> allPlayers = null;
            if (team == 0)
            {
                return RedirectToAction("Index");
            }
            if (team != 0)
            {
                allPlayers = from player in db.players.Include(p => p.player_state).Include(p => p.position).Include(p => p.team)
                             where player.team_id == team
                             select player;
            }


            //добавляем поля для фильтрации
            List<team> Teams = db.teams.ToList();
            //Добавляем в список возможность выбора всех
            Teams.Insert(0, new team { short_name = "Все", id = 0 });
            ViewBag.Teams = new SelectList(Teams, "id", "short_name");

            return View(allPlayers.ToList());
        }

        [AllowAnonymous]
        public ActionResult PartialIndex()
        {
            return PartialView();
        }

        // GET: Players/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            player player = db.players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Players/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.player_state_id = new SelectList(db.player_state, "id", "name_ru");
            ViewBag.position_id = new SelectList(db.positions, "id", "name_ru");
            ViewBag.team_id = new SelectList(db.teams, "id", "short_name");
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,team_id,date_of_birth,weight,height,position_id,photo_path,player_state_id")] player player)
        {
            if (ModelState.IsValid)
            {
                db.players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.player_state_id = new SelectList(db.player_state, "id", "name_ru", player.player_state_id);
            ViewBag.position_id = new SelectList(db.positions, "id", "name_ru", player.position_id);
            ViewBag.team_id = new SelectList(db.teams, "id", "short_name", player.team_id);
            return View(player);
        }

        // GET: Players/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            player player = db.players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.player_state_id = new SelectList(db.player_state, "id", "name_ru", player.player_state_id);
            ViewBag.position_id = new SelectList(db.positions, "id", "name_ru", player.position_id);
            ViewBag.team_id = new SelectList(db.teams, "id", "short_name", player.team_id);
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,team_id,date_of_birth,weight,height,position_id,photo_path,player_state_id")] player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.player_state_id = new SelectList(db.player_state, "id", "name_ru", player.player_state_id);
            ViewBag.position_id = new SelectList(db.positions, "id", "name_ru", player.position_id);
            ViewBag.team_id = new SelectList(db.teams, "id", "short_name", player.team_id);
            return View(player);
        }

        // GET: Players/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            player player = db.players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            player player = db.players.Find(id);
            db.players.Remove(player);
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
