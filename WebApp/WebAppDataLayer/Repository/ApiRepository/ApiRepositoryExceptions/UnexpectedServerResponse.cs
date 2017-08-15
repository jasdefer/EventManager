using System.Net;

namespace WebAppDataLayer.Repository.ApiRepository.ApiRepositoryExceptions
{
    public class UnexpectedServerResponse : ApiRepositoryException
    {
        public UnexpectedServerResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}
