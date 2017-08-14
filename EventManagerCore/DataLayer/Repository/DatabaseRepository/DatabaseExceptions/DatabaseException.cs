using System;

namespace DataLayer.Repository.DatabaseRepository.DatabaseExceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException()
        {

        }

        public DatabaseException(string message) : base(message)
        {

        }
    }
}
