using AppServer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Core
{
    public interface IApplicationDbContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<AccountRole> AccountRoles { get; set; }        
        DbSet<Role> Roles { get; set; }

        Task<int> SaveChangesAsync();
    }
}
