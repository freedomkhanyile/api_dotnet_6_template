using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Domain.Models
{
    public class AccountRole : AuditEntity<Guid>
    {
        public Guid AccountId { get; set; }
        public virtual Account? Account { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role? Role { get; set; }
    }
}
