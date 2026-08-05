using BankApp_Api.Core.DTO.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankApp_Api.Core.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TransactionDTO?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TransactionDTO>> GetByCardIdAsync(int cardId, CancellationToken cancellationToken = default);
        Task AddAsync (CreateTransactionDTO dto, CancellationToken cancellationToken = default);

    }
}
