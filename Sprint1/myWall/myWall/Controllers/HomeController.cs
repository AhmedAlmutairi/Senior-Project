using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using myWall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace myWall.Controllers
{
    public class HomeController : Controller
    {

        private MyWallContext db = new MyWallContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateWall()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateWall(Wall wall)
        {
            if (ModelState.IsValid)
            {
                db.Walls.Add(wall);

                db.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(wall);
        }


       

        public ActionResult Wall()
        {
            var wall = db.Walls.ToList();

            return View(wall);
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
    }
}