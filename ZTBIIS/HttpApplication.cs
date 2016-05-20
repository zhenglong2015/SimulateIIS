using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZTBIIS
{
    public class HttpApplication : IHttpHandler
    {
        //处理当前的请求
        public void ProcessRequest(HttpContext context)
        {
            //获得请求信息
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            if (string.IsNullOrEmpty(context.Request.Url)) return;
            string fileName = Path.Combine(basePath, context.Request.Url.TrimStart('/'));

            //处理动态文件
            string ext = Path.GetExtension(context.Request.Url);
            if (ext == ".aspx")
            {
                var className = Path.GetFileNameWithoutExtension(context.Request.Url);
                IHttpHandler obj = (IHttpHandler)Assembly.Load("ZTBIIS").CreateInstance("ZTBIIS."+className);
                obj.ProcessRequest(context);
                return;

            }


            if (!File.Exists(fileName))
            {
                context.Response.StateCode = "404";
                context.Response.StateDes = "Not Found";
                context.Response.ContextType = "text/html";
                context.Response.Body = new byte[1024];
            }
            else
            {
                context.Response.StateCode = "200";
                context.Response.StateDes = "OK";
                context.Response.ContextType = GetContentType(Path.GetExtension(context.Request.Url));
                context.Response.Body = File.ReadAllBytes(fileName);
            }
        }

        public string GetContentType(string ext)
        {
            string type = "text/html;charset=UTF-8";
            switch (ext)
            {
                case ".aspx":
                case ".html":
                case ".htm":
                    type = "text/html;charset=UTF-8";
                    break;
                case ".png":
                    type = "image/png";
                    break;
                default:
                    break;
            }

            return type;
        }
    }
}
