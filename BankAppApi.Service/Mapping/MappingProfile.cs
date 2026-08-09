using AutoMapper;
using BankApp_Api.Core.DTO.Transaction;
using BankApp_Api.Repository.Entities;
using BankAppApi.Core.DTO.Account;
using BankAppApi.Core.DTO.Card;
using BankAppApi.Core.DTO.Customer;
using BankAppApi.Core.DTO.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppApi.Service.Mapping
{
    public class MappingProfile : Profile
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
            CreateMap<TransactionType, TransactionTypeDTO>().ReverseMap();
        }

    }
}

