using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppDataLayer.Repository.ApiRepository
{
    public class TokenAccessor
    {
        public TokenAccessor()
        {
            int a = 5;
        }

        public string Token { get; private set; }

        public void SetToken(string token)
        {
            Token = token;
        }

    }
}
