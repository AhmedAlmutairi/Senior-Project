using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myWall.Controllers
{
    public class myActionController : Controller
    {
        public int App_Data { get; private set; }

        // GET: myAction

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