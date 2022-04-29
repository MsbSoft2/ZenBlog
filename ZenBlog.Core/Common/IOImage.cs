using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenBlog.Core.Common
{
    public class IOImage
    {
        public enum ImageMode
        {
            profile,
            post
        }
        public async Task<string> SaveImage(IFormFile image, ImageMode mode)
        {
            var imageName = "Default.png";
            if (image != null)
            {
                imageName = Guid.NewGuid() + Path.GetExtension(image.FileName);
            }
            var mainPath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot", "Images");
            switch (mode)
            {
                case ImageMode.profile:
                    mainPath = Path.Combine(mainPath, "Profiles", imageName);
                    imageName = Path.Combine("Images", "Profiles", imageName);
                    break;
                case ImageMode.post:
                    mainPath = Path.Combine(mainPath, "Posts", imageName);
                    imageName = Path.Combine("Images", "Posts", imageName);
                    break;
            }
            if (image != null)
            {
                using (var fileStream = new FileStream(mainPath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            }
            return $"\\{imageName}";
        }

        public void DeleteImage(string path)
        {
            if (!path.Contains("Default.png"))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot" + path);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
    }
}
