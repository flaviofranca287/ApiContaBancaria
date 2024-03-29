﻿using DesafioStone.Data.Dtos;
using Microsoft.AspNetCore.Mvc;

// Quem verifica se tem dinheiro no domínio aqui.
namespace DesafioStone.Data
{
    public interface IAccountServices
    {
        public CreateAccountResponse CreateAccount(CreateAccountRequest request);
    }
    // O meu objeto está implementando minha interface que é criar a conta e retornar uma response
    public class AccountServices : IAccountServices
    {
        private IAccountRepository _accountRepository;

        public AccountServices(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public CreateAccountResponse CreateAccount(CreateAccountRequest accountDto)
        {
            var account = _accountRepository.Add(new Models.Account(accountDto.OwnerOfAccount));


            var response = new CreateAccountResponse()
            {
                IdAccount = account.Id,
                OwnerOfAccount = account.OwnerOfAccount,
                Balance = account.Balance
            };
            return response;
        }
    }
}
