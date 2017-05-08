using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using myWall.Models;
using myWall.Repositories;
using myWall.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using PagedList;
using PagedList.Mvc;

namespace myWall.Controllers
{
    
    [RoutePrefix("Home")]
    //[ValidateInput(false)]
    
    public class HomeController : Controller
    {
        
        private MyWallContext db = new MyWallContext();
        ApplicationDbContext d = new ApplicationDbContext();

        
        public ActionResult Index(int? page)
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
                d.Walls.Add(wall);
                
                d.SaveChanges();

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

        
        [Route("Wall")]
        [HttpGet]
        public ActionResult Wall(int? id)
        {

            List<object> myModel = new List<object>();
            //var post = d.Walls.Find(id).Posts.ToList();
            //myModel.Add(d.Walls.ToList());
            //myModel.Add(d.Posts.ToList());


            var wall = from w in d.Walls
                       where w.Id == id
                       select w;
            var wal = wall.First();

            var post = from p in d.Posts
                       join w in d.Walls on p.WallId equals wal.Id
                       where p.WallId == id
                       select p;
            

            myModel.Add(wall.ToList());
            myModel.Add(post.ToList());
            /* var content = d.Walls.
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


            }).ToList();*/
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
            var q = from temp in d.Posts where temp.Id == Id select temp.Image;
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
        public ActionResult Create(Post model, int id)
        {


                Wall Id = db.Walls.Find(id);
            
                HttpPostedFileBase file = Request.Files["ImageData"];
                //var UserId = User.Identity.GetUserId();
                //model.UserId = User.Identity.GetUserId();
               // model.WallId = 
                //ContentRepository service = new ContentRepository();
                int i = myWall(file, model, id);
                if (i == 1)
                {

                    return RedirectToAction("Wall", new { id = id});
                }
            
            return View(model);
        }

        public int myWall(HttpPostedFileBase file, Post contentViewModel, int id)
        {
            if (User.Identity.IsAuthenticated)
            {

                contentViewModel.Image = ConvertToBytes(file);
                contentViewModel.UserId = User.Identity.GetUserId();
                contentViewModel.WallId = id;
                var Post = new Post
                {

                    UserId = contentViewModel.UserId,
                    WallId = contentViewModel.WallId,
                    Title = contentViewModel.Title,
                    Description = contentViewModel.Description,
                    Contents = contentViewModel.Contents,
                    Image = contentViewModel.Image
                };


                d.Posts.Add(Post);
                int i = d.SaveChanges();


                if (i == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

            return 0;

        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wall wal = d.Walls.Find(id);
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
           
            Wall wal = d.Walls.Find(id);
                d.Walls.Remove(wal);
                d.SaveChanges();
                return RedirectToAction("Index");
            
            
        }


        public ActionResult DeletePost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = d.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [HttpPost, ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedPost(int id)
        {
            Post post = d.Posts.Find(id);
            int i = post.WallId;
            d.Posts.Remove(post);
            d.SaveChanges();
            return RedirectToAction("Wall", new { id = i });
        }


        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = d.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost([Bind(Include = "Id, WallId, CallobId, Title, Image, Description, Contents")] Post post)
        {

            
            HttpPostedFileBase file = Request.Files["ImageData"];
            
            int d = post.WallId;
            int i = myWallEdit(file, post);
            if (i == 1)
            {


                return RedirectToAction("Wall", new { id = d });
                
            }


            return View(post);
        }

        public int myWallEdit(HttpPostedFileBase file, Post contentViewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                contentViewModel.Image = ConvertToBytes(file);
                contentViewModel.UserId = User.Identity.GetUserId();
                var Post = new Post
                {

                    UserId = contentViewModel.UserId,
                    WallId = contentViewModel.WallId, 
                    Title = contentViewModel.Title,
                    Description = contentViewModel.Description,
                    Contents = contentViewModel.Contents,
                    Image = contentViewModel.Image
                };


                d.Entry(contentViewModel).State = EntityState.Modified;
                int i = d.SaveChanges();


                if (i == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

            return 0;

        }

        public ActionResult uploadToCanvas(int? id)
        {
            string[] files = Directory.GetFiles(Server.MapPath("/Controllers/Files/"));
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = Path.GetFileName(files[i]);
            }
            ViewBag.Files = files;
            return View();
        }
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
                         var fileSavePath = Path.Combine(Server.MapPath("/Controllers/Files/"), file.FileName);
                         file.SaveAs(fileSavePath);
                         //return RedirectToAction("Wall");
                     }
                 }
             }
             return View();
         }

         public ActionResult Library()
         {
             string[] files = Directory.GetFiles(Server.MapPath("/Controllers/Files/"));
             for (int i = 0; i < files.Length; i++)
             {
                 files[i] = Path.GetFileName(files[i]);
             }
             ViewBag.Files = files;
             return View();
         }

         public FileResult DownloadFile(string fileName)
         {
             var filepath = System.IO.Path.Combine(Server.MapPath("/Controllers/Files/"), fileName);
             return File(filepath, MimeMapping.GetMimeMapping(filepath), fileName);
         }


      

        public ActionResult UserProfile(string id)
        {
            //var user = d.Users.Find(id);   
           // var user = User.Identity.GetUserId();
            //id = user;
            /*var wal = from w in d.Walls
                       
                       where w.UserId == 
                       select w;*/


            /*var user = from w in d.Users
                       where w.Id == id
                       select w;
            var users = user.First();

            var wal = from p in d.Walls
                       join w in d.Users on p.UserId equals users.Id
                       where p.UserId == id
                       select p;
            return View(wal.ToList());*/

            var user = d.Users.Find(id).Walls.ToList();
            return View(user);
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


        public ActionResult AWall()
        {
            return View();
        }
        public ActionResult Whiteboard()
        {
            string[] files = Directory.GetFiles(Server.MapPath("/Files"));
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = Path.GetFileName(files[i]);
            }
            ViewBag.Files = files;
            return View();

        }
        public ActionResult Chess()
        {

            return View();
        }

        public ActionResult Chat()
        {





            return View();
        }
       

    }
}