using myWall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myWall.Controllers
{
    public class HomeController : Controller
    {
        private DefaultConnection db = new DefaultConnection();

        // GET: Home
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

    }
}