using AppServer.Core.DTOs.Account;
using AppServer.Core.Helpers.Exceptions;
using AppServer.Core.Helpers.Extensions;
using AppServer.Core.Services.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Core.Features.AccountFeatures.Commands
{
    public class AuthenticateAccountCommand : IRequest<AccountDTO>
    {
        public Guid AccountId { get; set; }
    }
    public class AuthenticateAccountCommandHandler : IRequestHandler<AuthenticateAccountCommand, AccountDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthenticateAccountCommandHandler(ITokenService tokenService, IApplicationDbContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        public async Task<AccountDTO> Handle(AuthenticateAccountCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == command.AccountId);
                if (account == null) throw new NotFoundException($"Account does not exist with id: {command.AccountId}");

                var accountRoles = await _context.AccountRoles.Where(x => x.AccountId == account.Id)
                    .AsNoTracking()
                    .ToListAsync();
                if(accountRoles == null || accountRoles.Count == 0)
                    throw new NotFoundException($"No account roles linked to account with id: {command.AccountId}");
                var accountRoleIds = accountRoles.Select(x => x.RoleId).ToList();

                var roles = await _context.Roles.Where(x => accountRoleIds.Contains(x.Id))
                    .AsNoTracking()
                    .ToListAsync();
                if(roles == null || roles.Count == 0)
                    throw new NotFoundException($"No roles found");

                var roleNames = roles.Select(x => x.Name).ToArray();
                var expiryPeriod = DateTime.Now.ToLocalTime() + TokenAuthOption.ExpiresSpan;
                var token = _tokenService.BuildToken(account, roleNames!, expiryPeriod);

                if (token == null) throw new AppArgumentNullException("Token generation failed");

                account.Token = token;
                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();

                return account.ToModel();
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message, ex);
            }
        }
    }
}
