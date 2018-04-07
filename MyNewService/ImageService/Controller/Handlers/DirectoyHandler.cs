using ImageService.Modal;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System.Text.RegularExpressions;

namespace ImageService.Controller.Handlers
{
    public class DirectoyHandler : IDirectoryHandler
    {
        #region Members
        private IImageController m_controller;              // The Image Processing Controller
        private ILoggingService m_logging;
        private FileSystemWatcher[] m_dirWatchers;             // The Watcher of the Dir
        private string m_path;                              // The Path of directory

        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;              // The Event That Notifies that the Directory is being closed
        #endregion
        private static Regex r = new Regex(":");

        
        // The Event that will be activated upon new Command
        public DirectoyHandler(IImageController controller, ILoggingService logger, String inputPath)
        {
            this.m_controller = controller;
            this.m_logging = logger;
            this.m_path = inputPath;
        }

        public void StartHandleDirectory(string dirPath)
        {
            this.m_logging.Log("starting to handling the directory in path: " + dirPath, MessageTypeEnum.INFO);
            this.m_path = dirPath;

            string[] extension = {"*.jpg", "*.png", "*.gif", "*.bmp"};
            this.m_dirWatchers = new FileSystemWatcher[10];
            for (int i = 0; i < extension.Length ; i ++) {
            this.m_dirWatchers[i] = new FileSystemWatcher(this.m_path, extension[i])
            {
                IncludeSubdirectories = true,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite //notifies for creation of new file
            };
            this.m_dirWatchers[i].EnableRaisingEvents = true;

            this.m_dirWatchers[i].Changed += new FileSystemEventHandler(delegate (object sender, FileSystemEventArgs e)
            {
                string[] args = new string[4];
                args[0] = this.m_path;
                args[1] = e.Name;
                DateTime date = GetDateTakenFromImage(e.FullPath);
                args[2] = date.Year.ToString();
                args[3] = date.Month.ToString();
                bool result;
                this.m_logging.Log("new file in directory of path: " + this.m_path, MessageTypeEnum.INFO);
                this.m_controller.ExecuteCommand((int) CommandEnum.NewFileCommand, args, out result); 
            });
            
            }    
        }
        // The Function Recieves the directory to Handle



        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e) {
            switch (e.CommandID)
            {
                //close command
                case (int) CommandEnum.CloseCommand:
                    this.m_logging.Log("closing Directory Handler of directory in path: " + this.m_path, MessageTypeEnum.INFO);
                    DirectoryClose?.Invoke(this, new DirectoryCloseEventArgs(this.m_path, "closing"));
                    for (int i = 0; i< 4;i ++) {
                        this.m_dirWatchers[i].Dispose();
                    }
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        ////
    


        //retrieves the datetime WITHOUT loading the whole image
        private static DateTime GetDateTakenFromImage(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (System.Drawing.Image myImage = System.Drawing.Image.FromStream(fs, false, false))
            {
                System.Drawing.Imaging.PropertyItem propItem = myImage.GetPropertyItem(36867);
                string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                return DateTime.Parse(dateTaken);
            }
        }
    }
}
