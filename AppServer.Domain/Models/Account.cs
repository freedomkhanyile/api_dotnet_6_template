using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Domain.Models
{
    public class Account : AuditEntity<Guid>
    {

        [MaxLength(100)]
        public string? Email { get; set; }

        public string? Password { get; set; }      

        [MaxLength(15)]
        public string? PhoneNumber { get; set; }

        public string? Token { get; set; }

        public bool IsVerified { get; set; }

        public string? OTP { get; set; }
    }
}
