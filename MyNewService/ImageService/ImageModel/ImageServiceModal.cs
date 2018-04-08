using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
//using ImageService.Infrastructure;
//using System.Drawing.Imaging;

namespace ImageService.Modal
{
    public class ImageServiceModal : IImageServiceModal
    {
        #region Members
        public string OutputFolder { get; set; }            // The Output Folder
        public int thumbnailSize { get; set; }
        // The Size Of The Thumbnail Size
        #endregion

        public string AddFile(string[] args, out bool result)

        {
            string path = args[0] + "\\" + args[1];
            try
            {
                //System.IO.File.Copy(path, OutputFolder, true);
                if(File.Exists(path)) {
                //DateTime inputFileTime = File.GetCreationTime(path);
               // String year = inputFileTime.Year.ToString();
               // String month = inputFileTime.Month.ToString();
                String year = args[2];
                String month = args[3];
                Directory.CreateDirectory(OutputFolder);
                Directory.CreateDirectory(OutputFolder + "\\" + year);
                Directory.CreateDirectory(OutputFolder + "\\" + year + "\\" + month);
                Directory.CreateDirectory(OutputFolder + "\\" + "Thumbnails");
                Directory.CreateDirectory(OutputFolder + "\\" + "Thumbnails" + "\\" + year);
                Directory.CreateDirectory(OutputFolder + "\\" + "Thumbnails" + "\\" + year + "\\" + month);
                string finalOutputPath = OutputFolder + "\\" + year + "\\" + month + "\\";
                string finalOutputTPath = OutputFolder + "\\" + "Thumbnails" + "\\" + year + "\\" + month + "\\";
                
                String fileName = Path.GetFileName(path);                         
                while (File.Exists(finalOutputTPath + fileName)) {
                    fileName = "0" + fileName;
                }
                Image thumbnail;
                using (thumbnail = Image.FromFile(path))
                {
                    thumbnail = (Image)(new Bitmap(thumbnail, new Size(this.thumbnailSize, this.thumbnailSize)));
                    thumbnail.Save(finalOutputTPath + fileName);
                }


                fileName = Path.GetFileName(path); 
                while (File.Exists(finalOutputPath + fileName))
                {
                    fileName = "0" + fileName;
                }
                File.Move(path, finalOutputPath + fileName);    

                result = true;
                return "The file: " + Path.GetFileName(path) + " is now added to " + finalOutputPath +
                    " and also to the thumbnails folder";
                } else {
                    result = false;
                    return "path is not exist";
                }
            } catch (Exception ex) {
                result = false;
                return ex.ToString() + path;
            }
        }
    }
}
