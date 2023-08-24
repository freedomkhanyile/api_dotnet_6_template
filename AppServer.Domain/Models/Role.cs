using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Domain.Models
{
    public class Role : AuditEntity<Guid>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
