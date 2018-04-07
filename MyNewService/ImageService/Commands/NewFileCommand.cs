﻿//using ImageService.Infrastructure;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public class NewFileCommand : ICommand
    {
        private IImageServiceModal modal;

        public NewFileCommand(IImageServiceModal newModal)
        {
            modal = newModal;            // Storing the Modal
        }

        public string Execute(string[] args, out bool result)
        {
            return modal.AddFile(args[0] + "\\" + args[1], out result);
            // The String Will Return the New Path if result = true, and will return the error message
        }
    }
}
