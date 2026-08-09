using AutoMapper;
using BankApp_Api.Repository.Entities;
using BankAppApi.Core.DTO.Transaction;
using BankAppApi.Core.Repositories;
using BankAppApi.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppApi.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IGenericRepository<Transaction> _transactionRepository;
        private readonly IGenericRepository<Card> _cardRepository;
        private readonly IGenericRepository<Account> _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;   

        public TransactionService(IGenericRepository<Transaction> transactionRepository, IGenericRepository<Card> cardRepository, IGenericRepository<Account> accountRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _cardRepository = cardRepository;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddAsync(CreateTransactionDTO dto, CancellationToken cancellationToken = default)

        {
            var card = await _cardRepository.GetByIdAsync(dto.CardId,cancellationToken);

            // Check if the card exists and is not deleted
            if (card == null || card.IsDeleted)
            {
                throw new Exception("Card not found");
            }
            // Check if the card is active
            if (!card.CardStatus)
            {
                throw new Exception("Card is inactive. Transaction cannot be processed.");
            }
            
            if (dto.AccountId.HasValue)
            {
                var account = await _accountRepository.GetByIdAsync(dto.AccountId.Value, cancellationToken);
                                // Check if the account exists and is not deleted
                if (account == null || account.IsDeleted)
                {
                    throw new Exception("Account not found");
                }
                if(!account.AccountStatus)
                    {
                    throw new Exception("Account is inactive. Transaction cannot be processed.");
                }

                account.AccountBalance -= dto.TransactionAmount;
                account.UpdatedAt = DateTime.Now;
                _accountRepository.Update(account);
            }

            var transaction = _mapper.Map<Transaction>(dto);
            transaction.CreatedAt = DateTime.Now;
            transaction.UpdatedAt = DateTime.Now;

            await _transactionRepository.AddAsync(transaction, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }

        public async Task<IEnumerable<TransactionDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var transactions = await _transactionRepository.WhereAsync(x => !x.IsDeleted, cancellationToken);

            return _mapper.Map<IEnumerable<TransactionDTO>>(transactions);

        }

        public async Task<IEnumerable<TransactionDTO>> GetByCardIdAsync(int cardId, CancellationToken cancellationToken = default)
        {
            var transactions = await _transactionRepository.WhereAsync(x => !x.IsDeleted && x.CardId == cardId, cancellationToken);

            return _mapper.Map<IEnumerable<TransactionDTO>>(transactions);

        }

        public async Task<TransactionDTO?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id, cancellationToken);

            if (transaction == null || transaction.IsDeleted)
            {
                throw new Exception("Transaction not found"); 
            }
            
            return _mapper.Map<TransactionDTO>(transaction);
        }
    }
}

