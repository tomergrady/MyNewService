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
        public string m_OutputFolder { get; set; }            // The Output Folder
        public int m_thumbnailSize { get; set; }
        // The Size Of The Thumbnail Size
        #endregion

        public string AddFile(string path, out bool result)

        {
            try
            {
                //System.IO.File.Copy(path, m_OutputFolder, true);
                DateTime inputFileTime = File.GetCreationTime(path);
                String year = inputFileTime.Year.ToString();
                String month = inputFileTime.Month.ToString();
                Directory.CreateDirectory(m_OutputFolder);
                Directory.CreateDirectory(m_OutputFolder + "\\" + year);
                Directory.CreateDirectory(m_OutputFolder + "\\" + year + "\\" + month);
                Directory.CreateDirectory(m_OutputFolder + "\\" + "Thumbnails");
                Directory.CreateDirectory(m_OutputFolder + "\\" + "Thumbnails" + "\\" + year);
                Directory.CreateDirectory(m_OutputFolder + "\\" + "Thumbnails" + "\\" + year + "\\" + month);
                string finalOutputPath = m_OutputFolder + "\\" + year + "\\" + month + "\\";
                string finalOutputTPath = m_OutputFolder + "\\" + "Thumbnails" + year + "\\" + month + "\\";
                Image thumbnail = Image.FromFile(path);
                thumbnail = (Image)(new Bitmap(thumbnail, new Size(this.m_thumbnailSize, this.m_thumbnailSize)));
                thumbnail.Save(finalOutputTPath);
                File.Move(path, finalOutputPath);
                result = true;
                return "The file: " + Path.GetFileName(path) + " is now added to " + finalOutputPath +
                    " and also to the thumbnails folder";
            } catch (Exception ex)
            {
                result = false;
                return ex.ToString();
            }

        }
    }
}
