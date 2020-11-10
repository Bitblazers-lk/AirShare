using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirShare
{
    public class MessageBoxClass
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string BgColor { get; set; } = "white";
        public Dictionary<string, (string Color,Action Action)> Buttons { get; set; }
        public MessageBoxClass(string _Title, string _Message)
        {
            Title = _Title;
            Message = _Message;
            Buttons = new Dictionary<string, (string Color, Action Action)>() { { "OK", ("info", null) } };
        }
    }
    
}
