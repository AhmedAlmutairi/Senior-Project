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
        ApplicationDbContext d = ApplicationDbContext.Repository();

        [Route("Wall")]
        [HttpGet]
        public ActionResult Id( int? id)
        {
            if( id == null)
            {
                RedirectToAction("Index", "Home");
            }

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

                return RedirectToAction("Wall", new { id = id });
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
            Wall wall = db.Walls.Find(id);
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
                db.Entry(wall).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Question", wall.UserId);
            return View(wall);
        }

        // GET: Wall/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wall wall = db.Walls.Find(id);
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
            Wall wall = db.Walls.Find(id);
            db.Walls.Remove(wall);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
