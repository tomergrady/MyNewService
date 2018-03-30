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
        private IImageController m_controller;
        private ILoggingService m_logging;
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;
        private DirectoyHandler[] directoryHandlers;
        #endregion

        public ImageServer(IImageController controller, ILoggingService logger, string[] paths, int numOfPaths)
        {
            
            this.m_controller = controller;
            this.m_logging = logger;
            for (int i = 0; i < numOfPaths; i++)
            {
                // create handler for each path
                directoryHandlers[i] = new DirectoyHandler(m_controller, m_logging, paths[i]);
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
