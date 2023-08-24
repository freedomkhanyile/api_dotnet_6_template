using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Core.Helpers.Constants
{
    public static class StatusConstants
    {
        public static string ACTIVE = "Active";
        public static string DISABLED = "Disabled";
        public static string ARCHIVED = "Archived";
        public static string PENDING_ACTIVATION = "Pending Activation";

    }
    public enum StatusEnum
    {
        ACTIVE = 1,
        DISABLED = 2,
        ARCHIVED = 3,
        PENDING_ACTIVATION = 4,
    }
}
