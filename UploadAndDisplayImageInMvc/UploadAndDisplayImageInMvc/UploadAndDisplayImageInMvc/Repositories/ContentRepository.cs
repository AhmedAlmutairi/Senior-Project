using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using UploadAndDisplayImageInMvc.Models;
using UploadAndDisplayImageInMvc.ViewModel;

namespace UploadAndDisplayImageInMvc.Repositories
{
    public class ContentRepository
    {
        private readonly DBContext db = new DBContext();
        public int myWall(HttpPostedFileBase file, ContentViewModel contentViewModel)
        {
            contentViewModel.Image = ConvertToBytes(file);
            var Content = new Content
            {
                Title = contentViewModel.Title,
                Description = contentViewModel.Description,
                Contents = contentViewModel.Contents,
                Image = contentViewModel.Image
            };
            db.Contents.Add(Content);
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