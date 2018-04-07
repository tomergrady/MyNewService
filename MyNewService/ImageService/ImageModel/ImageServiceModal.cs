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

        public string AddFile(string path, out bool result)

        {
            try
            {
                //System.IO.File.Copy(path, OutputFolder, true);
                DateTime inputFileTime = File.GetCreationTime(path);
                String year = inputFileTime.Year.ToString();
                String month = inputFileTime.Month.ToString();
                Directory.CreateDirectory(OutputFolder);
                Directory.CreateDirectory(OutputFolder + "\\" + year);
                Directory.CreateDirectory(OutputFolder + "\\" + year + "\\" + month);
                Directory.CreateDirectory(OutputFolder + "\\" + "Thumbnails");
                Directory.CreateDirectory(OutputFolder + "\\" + "Thumbnails" + "\\" + year);
                Directory.CreateDirectory(OutputFolder + "\\" + "Thumbnails" + "\\" + year + "\\" + month);
                string finalOutputPath = OutputFolder + "\\" + year + "\\" + month + "\\";
                string finalOutputTPath = OutputFolder + "\\" + "Thumbnails" + year + "\\" + month + "\\";
                
                //new file
                Image thumbnail = Image.FromFile(path);
                thumbnail = (Image)(new Bitmap(thumbnail, new Size(this.thumbnailSize, this.thumbnailSize)));
                thumbnail.Save(finalOutputTPath + Path.GetFileName(path));
                //add check if 
                File.Move(path, finalOutputPath + Path.GetFileName(path));
                result = true;
                return "The file: " + Path.GetFileName(path) + " is now added to " + finalOutputPath +
                    " and also to the thumbnails folder";
            } catch (Exception ex)
            {
                result = false;
                return ex.ToString() + path;
            }

        }
    }
}
