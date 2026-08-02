using BankApp_Api.Core.DTO.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_Api.Core.Services
{
    internal interface IAccountService
    {
        Task<IEnumerable<AccountDTO>> GetAllAsync();
        Task<AccountDTO?> GetByIdAsync(int id);
        Task<IEnumerable<AccountDTO>> GetByCustomerIdAsync(int customerId);
        Task AddAsync(CreateAccountDTO dto);
        Task UpdateAsync(int id, UpdateAccountDTO dto);
        Task DeleteAsync(int id);
    }
}
