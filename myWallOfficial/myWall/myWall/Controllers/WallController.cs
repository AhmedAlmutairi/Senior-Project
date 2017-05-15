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
    public class WallController : Controller
    {
        private MyWallContext db = new MyWallContext();
        ApplicationDbContext d = new ApplicationDbContext();

        [Route("Wall")]
        [HttpGet]
        public ActionResult Id( int? id)
        {
            if( id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<object> myModel = new List<object>();
            //var post = d.Walls.Find(id).Posts.ToList();
            //myModel.Add(d.Walls.ToList());
            //myModel.Add(d.Posts.ToList());


            var walls = from w in d.Walls
                       where w.Id == id
                       select w;
            var wall = walls.First();

            var posts = from p in d.Posts
                       join w in d.Walls on p.WallId equals w.Id
                       where p.WallId == id
                       select p;

            var chats = from c in d.Chats
                        join w in d.Walls on c.WallId equals w.Id
                        where c.WallId == id
                        select c;

            myModel.Add(walls.ToList());
            myModel.Add(posts.ToList());
            myModel.Add(chats.ToList());
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

        public ActionResult Id()
        {
            return RedirectToAction("Index", "Home");
        }

        //// GET: Wall
        //public ActionResult Index()
        //{
        //    var walls = db.Walls.Include(w => w.AspNetUsers);
        //    return View(walls.ToList());
        //}

        //// GET: Wall/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Wall wall = db.Walls.Find(id);
        //    if (wall == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(wall);
        //}

        // GET: Wall/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Route("Create")]
        [HttpPost]
        public ActionResult Create(Wall wall)
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

                return RedirectToAction("Comment", "Wall", new { id = wall.Id });


            }
            return View(wall);
        }

        [HttpGet]
        public ActionResult Comment( )
        {
            return View();
        }

        [HttpPost]
        public ActionResult Comment(Post model, int id)
        {

            Wall Id = d.Walls.Find(id);

            HttpPostedFileBase file = Request.Files["ImageData"];
            //var UserId = User.Identity.GetUserId();
            //model.UserId = User.Identity.GetUserId();
            // model.WallId = 
            //ContentRepository service = new ContentRepository();
            int i = myWall(file, model, id);
            if (i == 1)
            {

                return RedirectToAction("Id", "Wall", new { id = id });
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

        //// POST: Wall/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,UserId")] Wall wall)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Walls.Add(wall);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Question", wall.UserId);
        //    return View(wall);
        //}

        // GET: Wall/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wall wall = d.Walls.Find(id);
            if (wall == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Question", wall.UserId);
            return View(wall);
        }

        // POST: Wall/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,UserId")] Wall wall)
        {
            if (ModelState.IsValid)
            {
                d.Entry(wall).State = EntityState.Modified;
                d.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Question", wall.UserId);
            return View(wall);
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


                return RedirectToAction("Id", "Wall", new { id = d });

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

        // GET: Wall/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wall wall = d.Walls.Find(id);
            if (wall == null)
            {
                return HttpNotFound();
            }
            return View(wall);
        }


        // POST: Wall/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wall wall = d.Walls.Find(id);
            var posts = from p in d.Posts
                        select p;

            foreach( Post post in posts)
            {
                d.Posts.Remove(post);
            }
            d.Walls.Remove(wall);
            d.SaveChanges();
            return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Id", "Wall", new { id = i });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
    }
}
