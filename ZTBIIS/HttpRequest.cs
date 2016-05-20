using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTBIIS
{
    public class HttpRequest
    {
        public HttpRequest(string requestStr)
        {
            string[] dates = requestStr.Replace("\r\n", ",").Split(',');

            HttpMethod = dates[0].Split(' ')[0];
            Url = dates[0].Split(' ')[1];
            HttpVersion = dates[0].Split(' ')[2];


        }
        public string HttpMethod { get; set; }
        public string Url { get; set; }
        public string HttpVersion { get; set; }

        public Dictionary<string, string> HeadDic { get; set; }
        public Dictionary<string, string> BodyDic { get; set; }
    }
}
