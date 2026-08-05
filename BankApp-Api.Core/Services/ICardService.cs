using BankApp_Api.Core.DTO.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_Api.Core.Services
{
    public interface ICardService
    {
        Task<IEnumerable<CardDTO>> GetAllAsync(CancellationToken cancellationToken=default);
        Task<CardDTO> GetAsync(int id, CancellationToken cancellationToken=default);

        Task<IEnumerable<CardDTO>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken=default);

        Task AddAsync (CreateCardDTO dto, CancellationToken cancellationToken=default);

        Task UpdateAsync (int id, UpdateCardDTO dto, CancellationToken cancellationToken=default);

        Task DeleteAsync (int id, CancellationToken cancellationToken=default);
        Task ToggleStatusAsync(int id, CancellationToken cancellationToken=default);

    }
}
