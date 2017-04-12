using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myWall.Models;

namespace myWall.Controllers
{
    public class RequestController : Controller
    {
        public int App_Data { get; private set; }

        private MyWallContext db = new MyWallContext();

        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Upload()
        {
            foreach (string upload in Request.Files)
            {
                if (Request.Files[upload].FileName != "")
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + "/ App_Data / uploads /";
                    string filename = Path.GetFileName(Request.Files[upload].FileName);
                    Request.Files[upload].SaveAs(Path.Combine(path, filename));
                }
            }
            return View("Upload");

        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Questions question)
        {

            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(question);
        }

        
        public ActionResult Index()
        {
            return View(db.Questions.ToList());
        }


        public ActionResult AWall()
        {
            return View();
        }
        public ActionResult Whiteboard()
        {
            return View();
        }
        public ActionResult Chess()
        {

            return View();
        }
    }
}