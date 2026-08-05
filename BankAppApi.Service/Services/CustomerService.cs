using AutoMapper;
using BankAppApi.Core.DTO.Customer;
using BankAppApi.Core.Repositories;
using BankAppApi.Core.Services;
using BankAppApi.Repository.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankAppApi.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache; // this is a interface for Redis cache
        private const string CacheKey = "CustomerList"; // this is a key for Redis cache

        public CustomerService(
            IGenericRepository<Customer> customerRepository, IUnitOfWork unitOfWork, IMapper mapper,IDistributedCache cache)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllAsync(CancellationToken cancellationToken= default)
        {
            // First rule is Ask to Redis Cache if there is a data in the cache with the key "CustomerList"
            var cachedData = await _cache.GetStringAsync(CacheKey, cancellationToken);

            if(!string.IsNullOrEmpty(cachedData))
            {
                // If there is a data in the cache, return it
                return JsonSerializer.Deserialize<IEnumerable<CustomerDTO>>(cachedData);
            }

            // Second Rule is If there isnt data in the cache, get it from the database

            var customers = await _customerRepository.WhereAsync(c => !c.IsDeleted, cancellationToken);
            var customerDTOs = _mapper.Map<IEnumerable<CustomerDTO>>(customers);

            // Third Rule is Save the data to the cache for future requests.
            // Also we can set an expiration time for the cache, for example 30 minutes.
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            };

            await _cache.SetStringAsync(CacheKey, JsonSerializer.Serialize(customerDTOs), cacheOptions, cancellationToken);

            // Finally return the data to the client
            return customerDTOs;
        }

        public async Task<CustomerDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);
            if (customer == null || customer.IsDeleted)
            {
                throw new Exception($"Customer not found.");
            }
            return _mapper.Map<CustomerDTO>(customer);
        }

        public async Task AddAsync(CreateCustomerDTO dto, CancellationToken cancellationToken = default)
        {
            var customer = _mapper.Map<Customer>(dto);
            customer.CreatedAt = DateTime.UtcNow;
            customer.UpdatedAt = DateTime.UtcNow;

            await _customerRepository.AddAsync(customer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Invalidate the cache after adding a new customer
            await _cache.RemoveAsync(CacheKey, cancellationToken);
        }
        public async Task UpdateAsync(int id, UpdateCustomerDTO dto, CancellationToken cancellationToken = default)
        {
            var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);
            if (customer == null || customer.IsDeleted)
            {
                throw new Exception($"Customer not found.");
            }

            customer.CustomerFullName = dto.CustomerFullName ?? customer.CustomerFullName;
            customer.CustomerBplace = dto.CustomerBplace ?? customer.CustomerBplace;
            customer.CustomerRiskLimit = dto.CustomerRiskLimit != 0 ? dto.CustomerRiskLimit : customer.CustomerRiskLimit;
            customer.UpdatedAt = DateTime.UtcNow;

            _customerRepository.Update(customer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Invalidate the cache after updating a customer
            await _cache.RemoveAsync(CacheKey, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);
            if (customer == null || customer.IsDeleted)
            {
                throw new Exception($"Customer not found.");
            }
            customer.IsDeleted = true;
            customer.UpdatedAt = DateTime.UtcNow;
            _customerRepository.Update(customer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Invalidate the cache after deleting a customer
            await _cache.RemoveAsync(CacheKey, cancellationToken);
        }

    }
}

