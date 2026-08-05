using AutoMapper;
using BankAppApi.Core.DTO.Card;
using BankAppApi.Core.Repositories;
using BankAppApi.Core.Services;
using BankAppApi.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppApi.Service.Services
{
    public class CardService : ICardService
    {
        private readonly IGenericRepository<Card> _cardRepository;
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CardService(IGenericRepository<Card> cardRepository, IGenericRepository<Customer> customerRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task AddAsync(CreateCardDTO dto, CancellationToken cancellationToken = default)
        {
            // İf CardType is (CardType=2) Credit Card, then check the CardLimit 

            if(dto.CardType == 2)
            {
                var customer = await _customerRepository.GetByIdAsync(dto.CustomerId, cancellationToken);

                if(customer == null)

                    throw new Exception("Customer not found");

                // Sum of all card limits for the customer

                var existingCards = await _cardRepository.WhereAsync(
                    c => c.CustomerId == dto.CustomerId
                      && c.CardType == 2
                      && !c.IsDeleted, cancellationToken);

                decimal totalCardLimit = existingCards.Where(x => x.CardLimit.HasValue).Sum(x => x.CardLimit!.Value);

                decimal newTotalCardLimit = dto.CardLimit ?? 0;

                if (totalCardLimit + newTotalCardLimit > customer.CustomerRiskLimit)

                        throw new Exception("Total card limit exceeds customer's risk limit");
            }

            var card = _mapper.Map<Card>(dto);

            card.CardNo = GenerateCardNumber();
            card.CardCcv = GenerateCardCcv();
            card.CardValidityMonth = (short)DateTime.Now.Month;
            card.CardValidityYear = (short)(DateTime.Now.Year + 5);
            card.CardStatus = true;
            card.CreatedAt = DateTime.Now;
            card.UpdatedAt = DateTime.Now;

            await _cardRepository.AddAsync(card, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var card = await _cardRepository.GetByIdAsync(id, cancellationToken);
            if(card == null || card.IsDeleted)
            {
                throw new Exception("Card not found");
            }
            card.IsDeleted = true;
            card.UpdatedAt = DateTime.Now;

            _cardRepository.Update(card);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<CardDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var cards= await _cardRepository.WhereAsync(x => !x.IsDeleted, cancellationToken);
            return _mapper.Map<IEnumerable<CardDTO>>(cards);
        }

        public Task<CardDTO> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var card = _cardRepository.GetByIdAsync(id, cancellationToken);
            if(card == null)
            {
                throw new Exception("Card not found");
            }

            return _mapper.Map<Task<CardDTO>>(card);
        }

        public async Task<IEnumerable<CardDTO>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default)
        {
            var cards = await _cardRepository.WhereAsync(x => x.CustomerId == customerId && !x.IsDeleted, cancellationToken);
            return _mapper.Map<IEnumerable<CardDTO>>(cards);
        }

        public async Task ToggleStatusAsync(int id, CancellationToken cancellationToken = default)
        {
             var card = await _cardRepository.GetByIdAsync(id, cancellationToken);
            if(card == null || card.IsDeleted)
            {
                throw new Exception("Card not found");
            }
            card.CardStatus = !card.CardStatus;
            card.UpdatedAt = DateTime.Now;

            _cardRepository.Update(card);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(int id, UpdateCardDTO dto, CancellationToken cancellationToken = default)
        {
            var card = await _cardRepository.GetByIdAsync(id, cancellationToken);

            if(card == null || card.IsDeleted)
            
                throw new Exception("Card not found");

            card.CardLimit = dto.CardLimit ?? card.CardLimit;
            card.UpdatedAt = DateTime.Now;

            _cardRepository.Update(card);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }

        // Generate a random 16-digit card number
        private string GenerateCardNumber()
        {
            var random = new Random();
            return $"{random.NextInt64(1000, 9999)} {random.NextInt64(1000, 9999)} {random.NextInt64(1000, 9999)} {random.NextInt64(1000, 9999)}";
        }

        // Generate a random 3-digit card CCV
        private string GenerateCardCcv()
        {
            var random = new Random();
            return random.Next(100, 999).ToString();
        }
    }
}

