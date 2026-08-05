using BankAppApi.Core.DTO.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppApi.Core.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDTO>> GetAllAsync(CancellationToken cancellationToken=default);
        Task<AccountDTO?> GetByIdAsync(int id, CancellationToken cancellationToken=default);
        Task<IEnumerable<AccountDTO>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken=default);
        Task AddAsync(CreateAccountDTO dto, CancellationToken cancellationToken=default);
        Task UpdateAsync(int id, UpdateAccountDTO dto, CancellationToken cancellationToken=default);
        Task ToggleStatusAsync (int id, CancellationToken cancellationToken=default);
        Task DeleteAsync(int id, CancellationToken cancellationToken=default);
    }
}

