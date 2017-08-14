using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Services.WebAppExceptions
{
    public class WebAppException : Exception
    {
        public WebAppException()
        {

        }

        public WebAppException(string message) : base(message)
        {
        }
    }
}
