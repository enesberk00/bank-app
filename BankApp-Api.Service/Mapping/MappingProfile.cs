using AutoMapper;
using BankApp_Api.Core.DTO.Account;
using BankApp_Api.Core.DTO.Card;
using BankApp_Api.Core.DTO.Customer;
using BankApp_Api.Core.DTO.Transaction;
using BankApp_Api.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_Api.Service.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CreateCustomerDTO, Customer>();

            CreateMap<Account, AccountDTO>();
            CreateMap<CreateAccountDTO, Account>();

            CreateMap<Card, CardDTO>();
            CreateMap<CreateCardDTO, Card>();   

            CreateMap<Transaction, TransactionDTO>();
            CreateMap<CreateTransactionDTO, Transaction>(); 
        }

    }
}
