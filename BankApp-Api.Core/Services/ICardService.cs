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
        Task<IEnumerable<CardDTO>> GetAllAsync();
        Task<CardDTO> GetAsync(int id);

        Task<IEnumerable<CardDTO>> GetByCustomerIdAsync(int customerId);

        Task AddAsync (CreateCardDTO dto);

        Task UpdateAsync (int id, UpdateCardDTO dto);

        Task DeleteAsync (int id);
        Task ToggleStatusAsync(int id);

    }
}
