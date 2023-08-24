using AppServer.Core.DTOs.Account;
using AppServer.Core.Helpers.Exceptions;
using AppServer.Core.Helpers.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Core.Features.AccountFeatures.Queries
{
    public class GetAccountByEmailQuery : IRequest<AccountDTO>
    {
        public string? Email { get; set; }
    }
    public class GetAccountByEmailQueryHandler : IRequestHandler<GetAccountByEmailQuery, AccountDTO>
    {
        private readonly IApplicationDbContext _context;

        public GetAccountByEmailQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AccountDTO> Handle(GetAccountByEmailQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Email == query.Email);
                if (account == null) return null!;
                return account.ToModel();
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message, ex);
            }
        }
    }
}
