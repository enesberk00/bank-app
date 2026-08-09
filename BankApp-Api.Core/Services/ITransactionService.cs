using BankApp_Api.Core.DTO.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankApp_Api.Core.Services
{
    internal interface ITransactionService
    {
        Task<IEnumerable<TransactionDTO>> GetAllAsync();
        Task<TransactionDTO?> GetByIdAsync(int id);
        Task<IEnumerable<TransactionDTO>> GetByCardIdAsync(int cardId);
        Task AddAsync (CreateTransactionDTO dto);

    }
}
