using BankAppApi.Core.DTO.Customer;
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
    public class CustomerController : ControllerBase
    {

        public readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _customerService.GetAllAsync(cancellationToken);
            return Ok(ApiResponse<IEnumerable<CustomerDTO>>.SuccessResult(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _customerService.GetByIdAsync(id, cancellationToken);

            return Ok(ApiResponse<CustomerDTO>.SuccessResult(result));
        }


        [HttpPost]

        public async Task<IActionResult> Add([FromBody] CreateCustomerDTO dto, CancellationToken cancellationToken)
        {
           await _customerService.AddAsync(dto, cancellationToken);
                        return Ok(ApiResponse<CustomerDTO>.SuccessResult(null, "Customer added successfully."));

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerDTO dto, CancellationToken cancellationToken)
        {
            await _customerService.UpdateAsync(id, dto, cancellationToken);
            return Ok(ApiResponse<CustomerDTO>.SuccessResult(null, "Customer updated successfully."));
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _customerService.DeleteAsync(id, cancellationToken);
            return Ok(ApiResponse<CustomerDTO>.SuccessResult(null, "Customer deleted successfully."));        
        }

    }
}
