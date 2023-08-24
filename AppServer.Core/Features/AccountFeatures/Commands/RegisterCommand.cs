using AppServer.Core.Helpers.Exceptions;
using AppServer.Core.Helpers.Extensions;
using AppServer.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace AppServer.Core.Features.AccountFeatures.Commands
{
    public class RegisterCommand : IRequest<Guid>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? HashedPassword { get; set; }
        [Required]
        public string? RoleName { get; set; }
        public string? Otp { get; set; }
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }
        public string? CreateUserId { get; set; }
        public string? ModifyUserId { get; set; }
        public bool IsActive { get; set; } = true;
        public int StatusId { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly Random _random = new Random();
        public RegisterCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            try
            {
                // Internal properties only assigned here.
                command.HashedPassword = command.Password != null ? BC.HashPassword(command.Password) : null;
                command.Otp = _random.Next().ToString().Substring(0, 4);

                var accountToCreate = command.ToEntity();

                await _context.Accounts.AddAsync(accountToCreate);
                await _context.SaveChangesAsync();

                if (accountToCreate.Id != Guid.Empty)
                {
                    var role = await _context.Roles.FirstOrDefaultAsync(x => x.Name!.ToLower() == command.RoleName!.ToLower());

                    if (role != null)
                    {
                        var accountRole = accountToCreate.ToEntity(role);
                        if (accountRole != null)
                            await _context.AccountRoles.AddAsync(accountRole);
                    }

                    await _context.SaveChangesAsync();
                    return accountToCreate.Id;
                }
                else return Guid.Empty;
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message, ex);
            }
        }
    }
}
