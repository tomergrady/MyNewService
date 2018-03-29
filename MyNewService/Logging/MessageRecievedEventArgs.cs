using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging.Modal
{
    public class MessageRecievedEventArgs : EventArgs
    {
        public MessageTypeEnum status { get; set; }
        public string message { get; set; }

        public MessageRecievedEventArgs(MessageTypeEnum typeEnum, string message)
        {
            this.status = typeEnum;
            this.message = message;
        }
    }
}
