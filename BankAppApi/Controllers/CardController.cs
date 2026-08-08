using BankAppApi.Core.DTO.Card;
using BankAppApi.Core.Services;
using BankAppApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BankApp_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {

        private readonly ICardService _cardService;

        public CardController(ICardService cardService)

        {
            _cardService = cardService;
        }


        [HttpGet]

        public  async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _cardService.GetAllAsync(cancellationToken);
            return Ok(ApiResponse<IEnumerable<CardDTO>>.SuccessResult(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _cardService.GetAsync(id, cancellationToken);
            return Ok(ApiResponse<CardDTO>.SuccessResult(result));
        }
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(int customerId, CancellationToken cancellationToken)
        {
            var result = await _cardService.GetByCustomerIdAsync(customerId, cancellationToken);
            return Ok(ApiResponse<IEnumerable<CardDTO>>.SuccessResult(result));
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCardDTO dto, CancellationToken cancellationToken)
        {
            await _cardService.AddAsync(dto, cancellationToken);

            string cardTypeName = dto.CardType == 1 ? "Debit Card" : "Credit Card";


            return Ok(ApiResponse<object>.SuccessResult(null, $"Your {cardTypeName} created successfully "));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCardDTO dto, CancellationToken cancellationToken)
        {
            await _cardService.UpdateAsync(id, dto, cancellationToken);
            return Ok(ApiResponse<object>.SuccessResult(null, "Card limit updated successfully"));
        }
        [HttpPatch("{id}/toggle-status")]
        public async Task<IActionResult> ToggleStatus(int id, CancellationToken cancellationToken)
        {
            await _cardService.ToggleStatusAsync(id, cancellationToken);
            return Ok(ApiResponse<object>.SuccessResult(null, "Card status changed successfully"));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _cardService.DeleteAsync(id, cancellationToken);
            return Ok(ApiResponse<object>.SuccessResult(null, "Card successfully cancelled."));
        }

    }
}
