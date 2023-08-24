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
    public class GetAccountByIdQuery : IRequest<AccountDTO>
    {
        public Guid AccountId { get; set; }
    }

    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDTO>
    {
        private readonly IApplicationDbContext _context;

        public GetAccountByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AccountDTO> Handle(GetAccountByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == query.AccountId);
                if (account == null) return null!;
                var accountModel = account.ToModel();
                return accountModel;
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message, ex);
            }
        }
    }
}
