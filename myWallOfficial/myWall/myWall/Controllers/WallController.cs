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

        /** Wall Routes **/

        [Route("Wall")]
        [HttpGet]
        public ActionResult Id( int? id)
        {
            if( id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<object> myModel = new List<object>();


            var walls = from w in d.Walls
                       where w.Id == id
                       select w;
            var wall = walls.First();

            var questions = from q in d.Posts
                            join w in d.Walls on q.Id equals w.QuestionId
                            where q.WallId == id
                            select q;

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
            myModel.Add(questions.ToList());

            return View(myModel);
        }

        public ActionResult Id()
        {
            return RedirectToAction("Index", "Home");
        }

        // GET: Wall/Create
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
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

                return RedirectToAction("Question", "Wall", new { id = wall.Id });


            }
            return View(wall);
        }

        // GET: Wall/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            // TODO Refactor
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
            if (wall.UserId != User.Identity.GetUserId())
            {
                return RedirectToAction("NotAuthorized", "Wall", id);
            }
            return View(wall);
        }

        // POST: Wall/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
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

        // GET: Wall/Delete/5
        [Authorize]
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
            if (wall.UserId != User.Identity.GetUserId())
            {
                return RedirectToAction("NotAuthorized", "Wall", id);
            }
            return View(wall);
        }


        // POST: Wall/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wall wall = d.Walls.Find(id);
            var posts = from p in d.Posts
                        select p;

            foreach (Post post in posts)
            {
                d.Posts.Remove(post);
            }
            d.Walls.Remove(wall);
            d.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        /** Comment Routes **/

        [Authorize]
        [HttpGet]
        public ActionResult Comment( )
        {
            return View();
        }

        [Authorize]
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

        [Authorize]
        [HttpGet]
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

            var userId = User.Identity.GetUserId();
            if (post.UserId == userId) {
                return View(post);
            }
            else
            {
                return RedirectToAction("NotAuthorized", "Wall", id);
            }
        }

        [Authorize]
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

        [Authorize]
        [HttpGet]
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
            if (post.UserId != User.Identity.GetUserId())
            {
                return RedirectToAction("NotAuthorized", "Wall", id);
            }
            return View(post);
        }

        [Authorize]
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

        /** Question Routes **/
        [Authorize]
        [HttpGet]
        public ActionResult Question()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Question(Post model, int id)
        {

            Wall wall = d.Walls.Find(id);
            //var qid = model.Id;
            //wall.QuestionId = model.Id;
            //d.SaveChanges();

            HttpPostedFileBase file = Request.Files["ImageData"];
            //var UserId = User.Identity.GetUserId();
            //model.UserId = User.Identity.GetUserId();
            // model.WallId = 
            //ContentRepository service = new ContentRepository();
            int i = myWall(file, model, id);
            if ( i > 0)
            {
                wall.QuestionId = i;
                d.SaveChanges();
            }
            //if (i == 1)
            //{

            //    return RedirectToAction("Id", "Wall", new { id = id });
            //}
            //return View(model);
            //return RedirectToAction("Id", "Wall", new { id = id });
            return RedirectToAction("Id", "Wall");

        }

        /** Unauthorized Routes **/

        [HttpGet]
        public ActionResult NotAuthorized()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult NotAllowed( int id)
        //{
        //    return View();
        //}

        /** Helper Methods **/

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
                    return Post.Id;
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


        [HttpGet]
        public ActionResult TodoList()
        {
            return View();
        }
        /// <summary>
        /// Save content and images
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult TodoList(Dolist model, string page)
        {
            page = ParseUrlForRoom(page);
            int id;
            var parse = int.TryParse(page, out id);




            int i = List(model, page);
            if (i == 1)
            {

                return RedirectToAction("Wall", new { id = 2 });
            }

            return View(model);
        }

        public int List(Dolist contentViewModel, string page)
        {

            page = ParseUrlForRoom(page);
            int id;
            var parse = int.TryParse(page, out id);

            if (User.Identity.IsAuthenticated)
            {


                contentViewModel.UserId = User.Identity.GetUserId();
                contentViewModel.WallId = id;
                //contentViewModel.Time = DateTime.Now;
                var Post = new Dolist
                {

                    UserId = contentViewModel.UserId,
                    WallId = contentViewModel.WallId,
                    Item = contentViewModel.Item,
                    Time = contentViewModel.Time,

                };


                db.Dolists.Add(Post);
                int i = db.SaveChanges();


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

        private string ParseUrlForRoom(string url)
        {
            var list = url.Split('/').ToList();
            var room = list.Last();
            return room;
        }

        public PartialViewResult Todaylist(int? id)
        {

            //ApplicationDbContext d = new ApplicationDbContext();
            var Id = d.Walls.Find(id);
            DateTime date = DateTime.Today;
            List<Dolist> model = db.Dolists.Where(x => x.Time == DateTime.Today && x.WallId == id).ToList();
            return PartialView("_List", model);
        }

        public PartialViewResult Yeslist(int? id)
        {
            //ApplicationDbContext d = new ApplicationDbContext();
            var Id = d.Walls.Find(id);
            DateTime yesterday = DateTime.Today.AddDays(-1);
            List<Dolist> model = db.Dolists.Where(x => x.Time == yesterday && x.WallId == id).ToList();
            return PartialView("_List", model);
        }

        public PartialViewResult Tomlist(int? id)
        {
            //ApplicationDbContext d = new ApplicationDbContext();
            var Id = d.Walls.Find(id);
            DateTime tom = DateTime.Today.AddDays(+1);
            List<Dolist> model = db.Dolists.Where(x => x.Time == tom && x.WallId == id).ToList();
            return PartialView("_List", model);
        }
    }
}
