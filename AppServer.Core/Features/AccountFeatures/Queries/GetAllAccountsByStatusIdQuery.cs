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
    public class GetAllAccountsByStatusIdQuery : IRequest<List<AccountDTO>>
    {
        public int StatusId { get; set; }
    }

    public class GetAllAccountsByStatusIdQueryHandler : IRequestHandler<GetAllAccountsByStatusIdQuery, List<AccountDTO>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllAccountsByStatusIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AccountDTO>> Handle(GetAllAccountsByStatusIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var accounts = await _context.Accounts.Where(x => x.StatusId == query.StatusId)
                    .AsNoTracking()
                    .ToListAsync();

                if (accounts == null) return null!;

                var accountListModel = accounts.Select(x => x.ToModel()).ToList();
                return accountListModel;
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message, ex);
            }
        }
    }
}
