using BankApp_Api.Core.DTO.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_Api.Core.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetAllAsync();
        Task<CustomerDTO> GetByIdAsync(int id);
        Task AddAsync (CreateCustomerDTO dto);
        Task UpdateAsync (int id, UpdateCustomerDTO dto);
        Task DeleteAsync (int id);

    }
}
