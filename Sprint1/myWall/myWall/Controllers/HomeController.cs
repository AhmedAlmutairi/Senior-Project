using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using myWall.Models;
using myWall.Repositories;
using myWall.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace myWall.Controllers
{
    
    //[RoutePrefix("Home")]
    //[ValidateInput(false)]
    
    public class HomeController : Controller
    {
        
        private MyWallContext db = new MyWallContext();
        ApplicationDbContext d = new ApplicationDbContext();

        public ActionResult Index()
        {
            
            var wall = d.Walls.ToList();
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


            //ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            if (User.Identity.IsAuthenticated)
            {
                //var userId = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
                //String WallId = currentUser.Id;
                //ApplicationUser AspNetUsers = UserManager.FindById(User.Identity.GetUserId());
                wall.UserId = User.Identity.GetUserId();
                db.Walls.Add(wall);
                
                db.SaveChanges();

                return RedirectToAction("Index");


            }
            return View(wall);
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [Route("{Home}/{Wall}/{id}")]
        [HttpGet]
        public ActionResult Wall(int? id)
        {

            List<object> myModel = new List<object>();
            myModel.Add(d.Walls.ToList());
            myModel.Add(db.Posts.ToList());



            /*var wall = db.Walls.Where().Select(w => new
            {
                d = w.Id,
                content = w.Posts.Select(s => new
                {
                    s.Id,
                    s.UserId,
                    s.WallId,
                    s.CallobId,
                    s.Title,
                    s.Image,
                    s.Contents,
                    s.Description
                })
            }).ToList();
            
            /*List<ContentViewModel> contentModel = content.Select(item => new ContentViewModel()
            {
                Id = item.Id,
                UserId = item.UserId,
                WallId = item.WallId,
                CallobId = item.CallobId,
                Title = item.Title,
                Image = item.Image,
                Description = item.Description,
                Contents = item.Contents
            }).ToList();*/
            //return View(wall);



            //var wal = db.Walls.Where(wa => wa.Id == id).ToList();
            //Find(id).Id.ToString().ToList();
            //return View(wal);*/

            var content = d.Walls.
           Join(d.Posts, u => u.Id, uir => uir.WallId,
           (u, uir) => new { u, uir }).
           Where(n => n.uir.WallId == n.u.Id)
           .AsEnumerable().Select(m => new Post  //ContentViewModel
           {
               Id = m.uir.Id,
               UserId = m.uir.UserId,
               WallId = m.uir.WallId,
               CallobId = m.uir.CallobId,
               Title = m.uir.Title,
               Image = m.uir.Image,
               Contents = m.uir.Contents,
               Description = m.uir.Description


           }).ToList();
            //return View(content);


            return View(myModel);

        }

        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        public byte[] GetImageFromDataBase(int Id)
        {
            var q = from temp in db.Posts where temp.Id == Id select temp.Image;
            byte[] cover = q.First();
            return cover;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Save content and images
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public ActionResult Create(Post model)
        {

            
            
                HttpPostedFileBase file = Request.Files["ImageData"];
                //var UserId = User.Identity.GetUserId();
                //model.UserId = User.Identity.GetUserId();
               // model.WallId = 
                ContentRepository service = new ContentRepository();
                int i = service.myWall(file, model);
                if (i == 1)
                {

                    return RedirectToAction("Wall");
                }
            
            return View(model);
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
            return RedirectToAction("Wall/id");
        }



         [HttpGet]
         public ActionResult Upload()
         {
             return View();
         }

         [HttpPost]
         public ActionResult Upload(string baseData)
         {
             if (HttpContext.Request.Files.AllKeys.Any())
             {
                 for (int i = 0; i <= HttpContext.Request.Files.Count; i++)
                 {
                     var file = HttpContext.Request.Files["files" + i];
                     if (file != null)
                     {
                         var fileSavePath = Path.Combine(Server.MapPath("/Files"), file.FileName);
                         file.SaveAs(fileSavePath);
                         //return RedirectToAction("Wall");
                     }
                 }
             }
             return View();
         }

         public ActionResult Library()
         {
             string[] files = Directory.GetFiles(Server.MapPath("/Files"));
             for (int i = 0; i < files.Length; i++)
             {
                 files[i] = Path.GetFileName(files[i]);
             }
             ViewBag.Files = files;
             return View();
         }

         public FileResult DownloadFile(string fileName)
         {
             var filepath = System.IO.Path.Combine(Server.MapPath("/Files/"), fileName);
             return File(filepath, MimeMapping.GetMimeMapping(filepath), fileName);
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