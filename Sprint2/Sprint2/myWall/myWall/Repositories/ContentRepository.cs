using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using myWall.ViewModel;
using myWall.Models;

namespace myWall.Repositories
{
    public class ContentRepository
    {
        private MyWallContext db = new MyWallContext();
        public int myWall(HttpPostedFileBase file, ContentViewModel contentViewModel)
        {
            contentViewModel.Image = ConvertToBytes(file);
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
            db.Posts.Add(Post);
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

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
    }
}