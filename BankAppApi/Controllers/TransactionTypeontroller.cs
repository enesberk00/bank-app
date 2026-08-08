using BankApp_Api.Core.DTO.Transaction;
using BankAppApi.Core.Services;
using BankAppApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApp_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionTypeontroller : ControllerBase
    {


        private readonly ITransactionTypeService _transactionTypeService;

        public TransactionTypeontroller(ITransactionTypeService transactionTypeService)
        {
            _transactionTypeService = transactionTypeService;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _transactionTypeService.GetAllAsync(cancellationToken);

            return Ok(ApiResponse<IEnumerable<TransactionTypeDTO>>.SuccessResult(result));
        
        
        
        }
    }
}
