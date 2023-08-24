using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Core.Helpers.Constants
{
    public class RoleConstants
    {
        public const string Admin = "Admin";
        public const string Member = "Member";
    }

    public enum RoleEnum
    {
        Admin,
        Member
    }
}
