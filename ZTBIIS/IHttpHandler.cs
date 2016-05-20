namespace ZTBIIS
{
    public interface IHttpHandler
    {
        void ProcessRequest(HttpContext context);
    }
}