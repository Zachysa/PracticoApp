using Microsoft.AspNetCore.Mvc;
using Practico.DatabaseController;
using Practico.Models;
using System.Diagnostics;

namespace Practico.Controllers
{

    public class HomeController : Controller
    {
        private readonly MyContext context;
        public tbUser tbUser { get; set; }

        public HomeController(MyContext dbContext)
        {
            this.context = dbContext;
            tbUser = new tbUser(dbContext);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User objUser)
        {
            if (ModelState.IsValid)
            {
                User? obj = tbUser.GetUserByNickNameAndPassword(objUser);
                if (obj != null)
                {
                    HttpContext.Session.SetString("UserId", obj.Id.ToString());
                    HttpContext.Session.SetString("UserName", obj.Name.ToString());
                    HttpContext.Session.SetString("IdRole", obj.IdRole.ToString());
                    ViewBag.err = "";
                    if (obj.IdRole == 3)
                        return RedirectToAction("EmployeeDashBoard");
                    else if (obj.IdRole == 2)
                        return RedirectToAction("BossDashBoard");
                }
            }
            ViewBag.err = "err";
            return View(objUser);
        }

        [HttpGet]
        public IActionResult BossDashBoard()
        {
            if (HttpContext.Session.GetString("UserId") != null && HttpContext.Session.GetString("UserId") != "")
            {
                return RedirectToAction("Index", "DashBoard");
            }
            else
            {
                return RedirectToAction("login");
            }
        }

        [HttpGet]
        public IActionResult EmployeeDashBoard()
        {
            if (HttpContext.Session.GetString("UserId") != null && HttpContext.Session.GetString("UserId") != "")
            {
                return RedirectToAction("Employee", "DashBoard");
            }
            else
            {
                return RedirectToAction("login");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}