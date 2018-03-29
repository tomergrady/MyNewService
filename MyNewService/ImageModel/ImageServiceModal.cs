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
        private string m_OutputFolder { get; set; }            // The Output Folder
        private int m_thumbnailSize { get; set; }              // The Size Of The Thumbnail Size

        public string AddFile(string path, out bool result)
        {
            //open a file
            result = true;
            return "";
        }

        #endregion

    }
}
