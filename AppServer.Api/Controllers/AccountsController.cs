using AppServer.Core.DTOs.Account;
using AppServer.Core.Features.AccountFeatures.Commands;
using AppServer.Core.Features.AccountFeatures.Queries;
using AppServer.Core.Helpers.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppServer.Api.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator, ILogger<AccountsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("list/{statusid}")]
        public async Task<ActionResult<List<AccountDTO>>> ListByStatusIdAync(int statusid)
        {
            try
            {
                return Ok(await _mediator.Send(new GetAllAccountsByStatusIdQuery { StatusId = statusid }));
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERR: Failed to fetch account records with status id: {statusid}");
                throw new ApplicationException(ex.Message, ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                return Ok(await _mediator.Send(new GetAccountByIdQuery { AccountId = id }));
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERR: Failed to fetch account record with id: {id}");
                throw new ApplicationException(ex.Message, ex);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AccountDTO>> LoginAsync([FromBody] LoginDTO model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var command = model.ToCommand();

                var accountId = await _mediator.Send(command);
                if (accountId == Guid.Empty) return Ok(new { Message = "Email or password is invalid!" });
                var account = await _mediator.Send(new AuthenticateAccountCommand { AccountId = accountId });
                if (account == null) return BadRequest(new
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Failed to authenticate user!"
                });
                return Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERR: Failed to login user");
                throw new ApplicationException(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<Guid>> RegisterAsync([FromBody] RegisterDTO model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var existingAccount = await _mediator.Send(new GetAccountByEmailQuery { Email = model.Email });
                if (existingAccount != null)
                {
                    return Ok(new
                    {
                        Message = $"Account with email : {model.Email} already in use"
                    });
                }
                var command = model.ToCommand();
                var accountId = await _mediator.Send(command);
                if (accountId.Equals(Guid.Empty)) return BadRequest(new
                {
                    Message = "Failed to create account record"
                });

                return Ok(accountId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERR: Failed to register user");                
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
