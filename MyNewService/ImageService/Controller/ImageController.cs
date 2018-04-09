using ImageService.Commands;
//using ImageService.Infrastructure;
//using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller
{
    public class ImageController : IImageController
    {
        private IImageServiceModal imageModal;                      // The Modal Object
        private Dictionary<int, ICommand> commands;

        public ImageController(IImageServiceModal modal)
        {
            imageModal = modal;                    // Storing the Modal Of The System
            commands = new Dictionary<int, ICommand>() {};
            commands[0] = new NewFileCommand(modal); 
        }

        public string ExecuteCommand(int commandID, string[] args, out bool resultSuccesful)
        {
            if (commands.Keys.Contains<int>(commandID)) {
                return commands[commandID].Execute(args, out resultSuccesful);
            } else {
                resultSuccesful = false;
                return "Command doesn't exist, failed";
            }
        }
    }
}
