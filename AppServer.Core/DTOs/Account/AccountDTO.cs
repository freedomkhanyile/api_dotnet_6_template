using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Core.DTOs.Account
{
    public class AccountDTO
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }
        public string? Token { get; set; }
        public bool IsVerified { get; set; }
        public string? OTP { get; set; }
        public DateTime CreateDate { get; set; }
        public string? CreateUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public string? ModifyUserId { get; set; }
        public bool IsActive { get; set; } = true;
        public int StatusId { get; set; }
    }
}
