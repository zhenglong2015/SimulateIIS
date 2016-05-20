using System.Text;

namespace ZTBIIS
{
    public class HttpResponse
    {
        public string StateCode { get; set; }

        public string StateDes { get; set; }

        public string ContextType { get; set; }
        //响应正文的内容
        public byte[] Body { get; set; }

        public byte[] GetResponseHeader()
        {
            if (!string.IsNullOrEmpty(StateCode))
            {
                string strRequesrHeader = string.Format(@"Http/1.1 {0} {1}
Content - Type:{2}
Accept - Ranges:bytes
Content - Encoding:gzip
Content - Length:{3}
Date: Fri, 20 May 2016 07:01:59 GMT
Last - Modified:Tue, 26 Apr 2016 08:16:23 GMT

", StateCode, StateDes, Body.Length, ContextType);
                return Encoding.Default.GetBytes(strRequesrHeader);
            }
            else
            {
                return new byte[1024];

            }
        }
    }
}