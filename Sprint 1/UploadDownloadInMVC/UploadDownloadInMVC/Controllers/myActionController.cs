using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UploadDownloadInMVC.Controllers
{
    public class myActionController : Controller
    {
        public int App_Data { get; private set; }

        // GET: myAction
        public ActionResult Index()
        {
            foreach (string upload in Request.Files)
            {
                if (Request.Files[upload].FileName != “”) 
               {
                    string path = AppDomain.CurrentDomain.BaseDirectory + “/App_Data/uploads/”;
                    string filename = Path.GetFileName(Request.Files[upload].FileName);
                    Request.Files[upload].SaveAs(Path.Combine(path, filename));
                }
            }
            return View(“Upload”);
        }
    }
}