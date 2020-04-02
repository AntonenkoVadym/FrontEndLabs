using GymProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymProject.Controllers
{
    public class HomeController : Controller
    {
        GymContext db = new GymContext();

        public ActionResult Index()
        {
            // получаем из бд все объекты Book
            IEnumerable<Coach> coaches = db.Coaches;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Coaches = coaches;
            // возвращаем представление
            return View();
        }

    }
}