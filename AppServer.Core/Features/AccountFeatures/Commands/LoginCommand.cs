using AppServer.Core.Helpers.Exceptions;
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
    public class LoginCommand : IRequest<Guid>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IEncryptionService _encryptionService;
        public LoginCommandHandler(IApplicationDbContext context, IEncryptionService encryptionService)
        {
            _context = context;
            _encryptionService = encryptionService;
        }

        public async Task<Guid> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Email!.ToLower() == command.Email!.ToLower());
                if (account == null) return Guid.Empty;
                if (command.Password != null)
                    if (!_encryptionService.IsValidPassword(command.Password, account.Password!))
                        return Guid.Empty;
                return account.Id;
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message, ex);
            }
        }
    }
}
