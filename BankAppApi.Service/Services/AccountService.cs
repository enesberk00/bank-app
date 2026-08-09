using AutoMapper;
using BankApp_Api.Repository.Entities;
using BankAppApi.Core.DTO.Account;
using BankAppApi.Core.Repositories;
using BankAppApi.Core.Services;
using BankAppApi.Repository.Entities;
using BankAppApi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppApi.Service.Services
{
    public class AccountService : IAccountService
    {

        private readonly IGenericRepository<Account> _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(IGenericRepository<Account> accountRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var accounts = await _accountRepository.WhereAsync(a => !a.IsDeleted, cancellationToken);
            return _mapper.Map<IEnumerable<AccountDTO>>(accounts);
        }

        public async Task<AccountDTO?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var account = await _accountRepository.GetByIdAsync(id, cancellationToken);
            if (account == null || account.IsDeleted)
                throw new KeyNotFoundException($"Account not found.");

            return _mapper.Map<AccountDTO>(account);
        }

        public async Task<IEnumerable<AccountDTO>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default)
        {
            var accounts = await _accountRepository.WhereAsync(a => a.CustomerId == customerId && !a.IsDeleted, cancellationToken);
            return _mapper.Map<IEnumerable<AccountDTO>>(accounts);
        }

        public async Task AddAsync(CreateAccountDTO dto, CancellationToken cancellationToken = default)
        {
            var account = _mapper.Map<Account>(dto);

            account.AccountNo = GenerateAccountNumber();
            account.AccountIban = GenerateIban(account.AccountNo);
            account.AccountStatus = true;
            account.CreatedAt = DateTime.Now;
            account.UpdatedAt = DateTime.Now;

            await _accountRepository.AddAsync(account, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(int id, UpdateAccountDTO dto, CancellationToken cancellationToken = default)
        {
            var account = await _accountRepository.GetByIdAsync(id, cancellationToken);
            if (account == null || account.IsDeleted)
                throw new KeyNotFoundException($"Account not found.");
            account.AccountName = dto.AccountName ?? account.AccountName;   
            account.UpdatedAt = DateTime.Now;

            _accountRepository.Update(account);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task ToggleStatusAsync(int id, CancellationToken cancellationToken = default)
        {
            var account = await _accountRepository.GetByIdAsync(id, cancellationToken);
            if (account == null || account.IsDeleted)
                throw new Exception("Account not found.");
            account.AccountStatus = !account.AccountStatus;
            account.UpdatedAt = DateTime.Now;
            _accountRepository.Update(account);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var account = await _accountRepository.GetByIdAsync(id, cancellationToken);
            if (account == null || account.IsDeleted)
                throw new KeyNotFoundException($"Account with id {id} not found.");
            account.IsDeleted = true;
            account.UpdatedAt = DateTime.Now;
            _accountRepository.Update(account);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        // Helper methods to generate account number and IBAN

        private string GenerateAccountNumber()
        {
            // Generate a random 10-digit account number
            var random = new Random();
            return random.NextInt64(1000000000, 9999999999).ToString();
        }

        private string GenerateIban(string accountNumber)
        {
            // For simplicity, we will generate a dummy IBAN based on the account number
            // In a real-world scenario, you would follow the IBAN generation rules for your country
            return $"TR00 0000 0000 {accountNumber}";
        }






    }
}

