using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Core.Helpers.Exceptions
{
    public class AppArgumentNullException : Exception
    {
        public AppArgumentNullException(string message) : base(message)
        {
        }
    }
}
