using BankAppApi.Core.DTO.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppApi.Core.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetAllAsync(CancellationToken cancellationToken=default);
        Task<CustomerDTO> GetByIdAsync(int id, CancellationToken cancellationToken=default);
        Task AddAsync (CreateCustomerDTO dto, CancellationToken cancellationToken=default);
        Task UpdateAsync (int id, UpdateCustomerDTO dto, CancellationToken cancellationToken=default);
        Task DeleteAsync (int id, CancellationToken cancellationToken=default);
    }
}

