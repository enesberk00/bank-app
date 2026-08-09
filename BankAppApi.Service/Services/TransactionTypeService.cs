using AutoMapper;
using BankApp_Api.Core.DTO.Transaction;
using BankApp_Api.Repository.Entities;
using BankAppApi.Core.Repositories;
using BankAppApi.Core.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_Api.Service.Services
{
    public class TransactionTypeService : ITransactionTypeService

    {
        private readonly IGenericRepository<TransactionType> _repository;
        private readonly IMapper _mapper;

        public TransactionTypeService(IGenericRepository<TransactionType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            
        }

        public async Task<IEnumerable<TransactionTypeDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {


            var types = await _repository.WhereAsync(t => !t.IsDeleted,cancellationToken);
            return _mapper.Map<IEnumerable<TransactionTypeDTO>>(types);

        }

    }
}
