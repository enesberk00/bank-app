using BankAppApi.Core.DTO.Transaction;
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
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
            
        }

        [HttpGet]

        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _transactionService.GetAllAsync(cancellationToken);
            return Ok(ApiResponse<IEnumerable<TransactionDTO>>.SuccessResult(result));

        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id ,CancellationToken cancellationToken)
        {
            var result = await _transactionService.GetByIdAsync(id , cancellationToken);

            return Ok(ApiResponse<TransactionDTO>.SuccessResult(result!));
        }

        [HttpGet("card/{cardId}")]

        public async Task<IActionResult> GetByCardId( int cardId ,CancellationToken cancellationToken)
        {

            var result= await _transactionService.GetByCardIdAsync(cardId , cancellationToken);
            return Ok(ApiResponse<IEnumerable<TransactionDTO>>.SuccessResult(result));
        }

        [HttpPost]

        public async Task <IActionResult> Add([FromBody]CreateTransactionDTO dto,CancellationToken cancellationToken)
        {
            await _transactionService.AddAsync(dto, cancellationToken);
            return Ok(ApiResponse<object>.SuccessResult(null,"Transaction successfully completed "));

        }
    }
}
