using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace csharp_webserver
{
    public class PreconditionException : Exception
    {
        public PreconditionException(string message) : base(message)
        {

        }
    }
}