using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Core.DTOs.Account
{
    public class RegisterDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }

        [Required]
        public string? RoleName { get; set; }
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }      
        public string? CreateUserId { get; set; }        
        public string? ModifyUserId { get; set; }
        public bool IsActive { get; set; } = true;
        public int StatusId { get; set; }
    }
}
