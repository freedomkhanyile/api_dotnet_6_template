using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Core.DTOs.Role
{
    public class CreateRoleDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }        
        public string? CreateUserId { get; set; }        
        public string? ModifyUserId { get; set; }
        public bool IsActive { get; set; } = true;
        public int StatusId { get; set; }
    }
}
