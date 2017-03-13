﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using myWall.Models;
using myWall.Repositories;
using myWall.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace myWall.Controllers
{
    [RoutePrefix("Home")]
    [ValidateInput(false)]

    public class HomeController : Controller
    {
        
        private MyWallContext db = new MyWallContext();

        public ActionResult Index()
        {
            var wall = db.Walls.ToList();
            return View(wall);
        }
        public ActionResult AWall()
        {
            return View();
        }
        public ActionResult Whiteboard()
        {
            return View();
        }
        public ActionResult Draw()
        {
            
            return View();
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



        [Route("Wall")]
        [HttpGet]
        public ActionResult Wall()
        {

            var content = db.Posts.Select(s => new
            {
                s.Id,
                s.UserId,
                s.WallId,
                s.CallobId,
                s.Title,
                s.Image,
                s.Contents,
                s.Description
            });

            List<ContentViewModel> contentModel = content.Select(item => new ContentViewModel()
            {
                Id = item.Id,
                UserId = item.UserId,
                WallId = item.WallId,
                CallobId = item.CallobId,
                Title = item.Title,
                Image = item.Image,
                Description = item.Description,
                Contents = item.Contents
            }).ToList();
            return View(contentModel);



            /*var wal = db.Walls.Where(wa => wa.Id == Id).ToList();
            //Find(id).Id.ToString().ToList();
            return View(wal);*/
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
        public ActionResult Create(ContentViewModel model)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
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