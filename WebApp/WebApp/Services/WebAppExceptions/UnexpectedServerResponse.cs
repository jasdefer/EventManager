using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Services.WebAppExceptions
{
    public class UnexpectedServerResponse : WebAppException
    {
        public UnexpectedServerResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public object StatusCode { get; }
    }
}
