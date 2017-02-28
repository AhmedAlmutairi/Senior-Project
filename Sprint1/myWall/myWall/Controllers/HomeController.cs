using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using myWall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var wall = db.Walls.ToList();
            return View(wall);
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


       

        public ActionResult Wall(int? Id)
        {
            var wal = db.Walls.Where(wa => wa.Id == Id).ToList();
            //Find(id).Id.ToString().ToList();
            return View(wal);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wall wal = db.Walls.Find(id);
            if (wal == null)
            {
                return HttpNotFound();
            }
            return View(wal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wall wal = db.Walls.Find(id);
            db.Walls.Remove(wal);
            db.SaveChanges();
            return RedirectToAction("Index");
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