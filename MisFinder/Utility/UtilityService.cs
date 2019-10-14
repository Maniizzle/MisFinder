using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MisFinder.Utility
{
    public class UtilityService : IUtility
    {
        private readonly IHostingEnvironment environment;

        public UtilityService(IHostingEnvironment environment)
        {
            this.environment = environment;
        }
        public string SaveImageToFolder(IFormFile file)
        {
            try
            {
                // Getting Filename
                var fileName = file.FileName;
                // Getting Extension
                var fileExtension = Path.GetExtension(fileName);
                // Unique filename "Guid"
                var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                // Concating filename + fileExtension (unique filename)
                var newFileName = string.Concat(myUniqueFileName, fileExtension);
                //  Generating Path to store photo 
                var filepath = Path.Combine(environment.WebRootPath, "Photos") + $@"\{newFileName}";

                if (!string.IsNullOrEmpty(filepath))
                {
                    // Storing Image in Folder
                    StoreInFolder(file, filepath);
                    return newFileName;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }


        }
        /// <summary>
        /// Saving captured image into Folder.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        public void StoreInFolder(IFormFile file, string fileName)
        {
            using (FileStream fs = System.IO.File.Create(fileName))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
        }
        public bool IsImageExtensionAllowed(IFormFile file)
        {
            // Getting Filename
            var fileName = file.FileName;
            // Getting Extension
            var fileExtension = Path.GetExtension(fileName);
            string[] allowedFileExtensions = new string[] { ".jpg",".jpeg", ".gif", ".png",  ".bmp" };
            return allowedFileExtensions.Contains(fileExtension.ToLower()) ? true : false;
               
        }
        public bool IsSizeAllowed(IFormFile file)
        {
            var maxLength = 1024 * 1024 * 5;  //5MB
                return file.Length > maxLength ? false : true;
        }
        }
}
