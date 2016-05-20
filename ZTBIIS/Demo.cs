using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTBIIS
{
    public class Demo : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string str = "<html><head><body><h1>"+DateTime.Now.ToString()+"</h1></body></head></html>";

            context.Response.StateCode = "200";
            context.Response.StateDes = "OK";
            context.Response.ContextType = "text/html";
            context.Response.Body = Encoding.UTF8.GetBytes(str);
        }
    }
}
