using System;

namespace PhotoSearch.Services
{
    public class ServiceException : Exception
    {
        public ServiceException(string msg) : base(msg)
        {

        }
    }
}
