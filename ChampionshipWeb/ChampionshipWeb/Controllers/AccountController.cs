using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using ChampionshipWeb.Models;

namespace ChampionshipWeb.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LogViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Players");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Players");
        }


        private bool ValidateUser(string login, string password)
        {
            bool isValid = false;

            using (championshipEntities _db = new championshipEntities())
            {
                try
                {
                    user user_ = (from u in _db.users
                                 where u.login == login && u.password == password
                                 select u).FirstOrDefault();

                    if (user_ != null)
                    {
                        isValid = true;
                    }
                }
                catch
                {
                    isValid = false;
                }
            }
            return isValid;
        }
    }
}