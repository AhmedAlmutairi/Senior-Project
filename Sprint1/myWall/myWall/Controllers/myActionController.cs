using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myWall.Models;

namespace myWall.Controllers
{
    public class myActionController : Controller
    {
        public int App_Data { get; private set; }

        private MyWallContext db = new MyWallContext();

        public ActionResult Create()
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

        public ActionResult SecurityQuestions()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SecurityQuestions(Questions question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("ViewSecurityQuestions");
            }
            return View(question);
        }

        public ActionResult ViewSecurityQuestions()
        {
            return View(db.Questions.ToList());
        }

        /// <summary>
        /// GET: Request/Create
        /// Gets the create form page for creating a new change of major request
        /// </summary>
        /// <returns>The View object for Request/Create</returns>
        [HttpGet]
        public ActionResult ViewSecurity ()
        {
            return View();
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