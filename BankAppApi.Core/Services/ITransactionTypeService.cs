using BankApp_Api.Core.DTO.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppApi.Core.Services
{
    public interface ITransactionTypeService
    {
        Task<IEnumerable<TransactionTypeDTO>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}