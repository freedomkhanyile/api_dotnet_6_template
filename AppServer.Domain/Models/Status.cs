using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Domain.Models
{
    public class Status
    {
        public int Id { get; set; }        
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? StatusClass { get; set; }
        public bool IsActive { get; set; }
    }
}
