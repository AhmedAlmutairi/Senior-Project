using FakeNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FakeNews.Controllers
{
    public class HomeController : Controller
    {
        private HeadlineContext db = new HeadlineContext();
        public ActionResult Index(string searchBy, string search)
        {
            if(searchBy == "Title")
            {
                return View(db.Headlines.Where(p => p.Title.StartsWith(search)).ToList());
            }
            else
            {
                return View(db.Headlines.Where(p => p.Body.StartsWith(search) || search == null).ToList());
            }
        }

        public ActionResult Tiltle(int? id)
        {
            var title = db.Headlines.Find(id);
            return View(title);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Submit()
        {
            ViewBag.Message = "Your Submit a headline page!";

            return View();

        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
       
        public ActionResult Create(Headline headline)
        {
            if (ModelState.IsValid)
            {
                db.Headlines.Add(headline);

                db.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(headline);
        }

    }
}