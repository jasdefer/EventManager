using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.DatabaseRepository.DatabaseExceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException() :base()
        {

        }

        public DatabaseException(string message) : base(message)
        {

        }
    }
}
