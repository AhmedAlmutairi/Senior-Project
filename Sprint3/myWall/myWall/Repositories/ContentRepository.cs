using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using myWall.ViewModel;
using myWall.Models;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;

namespace myWall.Repositories
{
   /* public class ContentRepository
    {
        //private MyWallContext db = new MyWallContext();
        ApplicationDbContext d = new ApplicationDbContext();
        public int myWall(HttpPostedFileBase file, Post contentViewModel)
        {
            contentViewModel.Image = ConvertToBytes(file);
            //contentViewModel.UserId = User.Identity.GetUserId();
            var Post = new Post
            {
                
                UserId = contentViewModel.UserId,
                WallId = contentViewModel.WallId,
                CallobId = contentViewModel.CallobId,
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

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
    }*/
}