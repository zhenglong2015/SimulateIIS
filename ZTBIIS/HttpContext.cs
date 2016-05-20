using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTBIIS
{
    public class HttpContext
    {
        public HttpRequest Request { get; set; }
        public HttpResponse Response { get; set; }

        public HttpContext(string str)
        {
            Request = new HttpRequest(str);
            Response = new HttpResponse();
        }
    }
}
