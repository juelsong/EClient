using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EConsole.Model
{
    public class Response
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public long timestamp { get; set; }
        public object result { get; set; }
        public string requestId { get; set; }
    }
}
