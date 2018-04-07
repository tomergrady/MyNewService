using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Logging.Modal;
using ImageService.Infrastructure.Enums;

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
            this.directoryHandlers = new DirectoyHandler[10];
            this.m_controller = controller;
            this.m_logging = logger;
            for (int i = 0; i < numOfPaths; i++)
            {
                // create handler for each path
                directoryHandlers[i] = new DirectoyHandler(m_controller, m_logging, paths[i]);
                directoryHandlers[i].StartHandleDirectory(paths[i]);
                this.CommandRecieved += directoryHandlers[i].OnCommandRecieved;
                directoryHandlers[i].DirectoryClose += this.RemoveDirectoryHandler;
            }
        }

        public void CloseServer()
        {
            this.m_logging.Log("starting closing server", MessageTypeEnum.INFO);
            CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, null, "");
            this.CommandRecieved?.Invoke(this, commandRecievedEventArgs);
        }

        public void CloseAllHandlers()
        {

        }

        private void RemoveDirectoryHandler(object sender, DirectoryCloseEventArgs messageArgs)
        {
            IDirectoryHandler sendingDirectoryHandler = sender as IDirectoryHandler;
            if (sendingDirectoryHandler == null)
            {
                //an object that isn't supposed to activate the event did it
                this.m_logging.Log("unotherized object tried to announce about closing of directory in path: " + messageArgs.DirectoryPath, MessageTypeEnum.WARNING);
                return;
            }
            this.m_logging.Log("Directory Handler of Directory in: " + messageArgs.DirectoryPath + @" with message: " + messageArgs.Message, MessageTypeEnum.INFO);
            //unsubscribing of the DirectoryHandler from the server message feed
            this.CommandRecieved -= sendingDirectoryHandler.OnCommandRecieved;
            if (this.CommandRecieved == null) {
                //if all the Directory Handlers closed succefully the server itself can finally close
                 this.m_logging.Log("closing server finally", MessageTypeEnum.INFO);
            }
        }

    }
}
