using BankAppApi.Core.DTO.Account;
using BankAppApi.Core.Services;
using BankAppApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApp_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _accountService.GetAllAsync(cancellationToken);
            return Ok(ApiResponse<IEnumerable<AccountDTO>>.SuccessResult(result));
        }

        [HttpGet("{id}")]
        

        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _accountService.GetByIdAsync(id, cancellationToken);
            return Ok(ApiResponse<AccountDTO>.SuccessResult(result));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateAccountDTO dto, CancellationToken cancellationToken)
        {
            await _accountService.AddAsync(dto, cancellationToken);
            return Ok(ApiResponse<AccountDTO>.SuccessResult(null, "Account added successfully."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAccountDTO dto, CancellationToken cancellationToken)
        {
            await _accountService.UpdateAsync(id, dto, cancellationToken);
            return Ok(ApiResponse<AccountDTO>.SuccessResult(null, "Account updated successfully."));
        }

        [HttpPatch("{id}/toggle-status")]
        public async Task<IActionResult> ToggleStatus(int id, CancellationToken cancellationToken)
        {
            await _accountService.ToggleStatusAsync(id, cancellationToken);
            return Ok(ApiResponse<AccountDTO>.SuccessResult(null, "Account status toggled successfully."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _accountService.DeleteAsync(id, cancellationToken);
            return Ok(ApiResponse<AccountDTO>.SuccessResult(null, "Account deleted successfully."));
        }
    }
}
