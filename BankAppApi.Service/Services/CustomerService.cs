using AutoMapper;
using BankApp_Api.Repository.Entities;
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
        private readonly IDistributedCache _cache;
        private const string CacheKey = "CustomerList";

        public CustomerService(
            IGenericRepository<Customer> customerRepository, IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache cache)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            // First rule: Ask Redis Cache if there is data with the key "CustomerList"
            var cachedData = await _cache.GetStringAsync(CacheKey, cancellationToken);

            if (!string.IsNullOrEmpty(cachedData))
            {
                // If there is data in the cache, return it
                return JsonSerializer.Deserialize<IEnumerable<CustomerDTO>>(cachedData);
            }

            // Second Rule: If there isn't data in the cache, get it from the database
            var customers = await _customerRepository.WhereAsync(c => !c.IsDeleted, cancellationToken);
            var customerDTOs = _mapper.Map<IEnumerable<CustomerDTO>>(customers);

            // Third Rule: Save the data to the cache for future requests (30 min expiry)
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            };

            await _cache.SetStringAsync(CacheKey, JsonSerializer.Serialize(customerDTOs), cacheOptions, cancellationToken);

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
            customer.CreatedAt = DateTime.Now;
            customer.UpdatedAt = DateTime.Now;

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
            customer.UpdatedAt = DateTime.Now;

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
            customer.UpdatedAt = DateTime.Now;
            _customerRepository.Update(customer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Invalidate the cache after deleting a customer
            await _cache.RemoveAsync(CacheKey, cancellationToken);
        }
    }
}
