using ImageService.Controller;
using ImageService.Controller.Handlers;
//using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Server
{
    public class ImageServer
    {

        #region Members
        private IImageController controller;
        private ILoggingService logger;
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;
        private DirectoyHandler[] directoryHandlers;
        #endregion

        public ImageServer(IImageController controller, ILoggingService logger, string[] paths, int numOfPaths)
        {
            
            this.controller = controller;
            this.logger = logger;
            for (int i = 0; i < numOfPaths; i++)
            {
                // create handler for each path
                directoryHandlers[i] = new DirectoyHandler(this.controller, this.logger, paths[i]);
                this.CommandRecieved += directoryHandlers[i].OnCommandRecieved;

            }
        }

        public void CloseServer()
        {

        }

        public void CloseAllHandlers()
        {

        }

    }
}
