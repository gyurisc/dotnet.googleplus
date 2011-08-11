using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GooglePlusStats.Models;
using GooglePlus; 

namespace GooglePlusStats.Controllers
{
    public class HomeController : Controller
    {
        GooglePlus.GooglePlusDb db = new GooglePlus.GooglePlusDb(Properties.Settings.Default.GooglePlusDB);

        public ActionResult Index() 
        {
            ViewBag.Message = "Google+ Statistics";
            List<Connection> connections = db.Connections.ToList();
            List<Person> persons = db.Persons.ToList();

            return View(persons);
        }

        public ActionResult About()
        {

            return View();
        }
    }
}
