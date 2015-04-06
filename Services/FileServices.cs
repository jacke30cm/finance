using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services
{
    public class ImageService
    {

        private readonly int MaxFileSize;
        private readonly string[] AcceptedFiles;


        public ImageService()
        {
            MaxFileSize = 2; //MB
            AcceptedFiles = new[]
            {
                ".jpg",
                ".png", 
                ".gif",
                ".jpeg",
                ".bmp"

            };
        }

        public string UploadImage(HttpPostedFileBase file, string destination)
        {
            if (file == null || file.ContentLength <= 0 || file.ContentLength >= (MaxFileSize * 1000000)) return null;

            var fileExtension = Path.GetExtension(file.FileName);
            var isAcceptedExtension = false;



            foreach (var allowedExtension in AcceptedFiles.Where(allowedExtension => allowedExtension == fileExtension))
            {
                isAcceptedExtension = true;
            }

            // If the uploaded file-extension does not match accepted ones, method will not continue 
            if (!isAcceptedExtension) return null;

            var uniqeFileName = System.Guid.NewGuid().ToString("N") + fileExtension;
            var folder = "";

            switch (destination)
            {
                case "User":
                    folder = GfxHelper.User + "/";
                    break;

                case "Contest":
                    folder = GfxHelper.Contest + "/";
                    break;

            }

            var savepath = HttpContext.Current.Server.MapPath("~" + folder + uniqeFileName);


            file.SaveAs(savepath);
            return uniqeFileName;

        }

    }
}
