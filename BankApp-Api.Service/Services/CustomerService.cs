using AutoMapper;
using BankApp_Api.Core.DTO.Customer;
using BankApp_Api.Core.Repositories;
using BankApp_Api.Core.Services;
using BankApp_Api.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_Api.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(
            IGenericRepository<Customer> customerRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<CustomerDTO>>(customers);
        }

        public async Task<CustomerDTO> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null || customer.IsDeleted)
            {
                throw new Exception($"Customer not found.");
            }
            return _mapper.Map<CustomerDTO>(customer);
        }

        public async Task AddAsync(CreateCustomerDTO dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            customer.CreatedAt = DateTime.UtcNow;
            customer.UpdatedAt = DateTime.UtcNow;

            await _customerRepository.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, UpdateCustomerDTO dto)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null || customer.IsDeleted)
            {
                throw new Exception($"Customer not found.");
            }

            customer.CustomerFullName = dto.CustomerFullName ?? customer.CustomerFullName;
            customer.CustomerBplace = dto.CustomerBplace ?? customer.CustomerBplace;
            customer.CustomerRiskLimit = dto.CustomerRiskLimit != 0 ? dto.CustomerRiskLimit : customer.CustomerRiskLimit;
            customer.UpdatedAt = DateTime.UtcNow;

            _customerRepository.Update(customer);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null || customer.IsDeleted)
            {
                throw new Exception($"Customer not found.");
            }
            customer.IsDeleted = true;
            customer.UpdatedAt = DateTime.UtcNow;
            _customerRepository.Update(customer);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
