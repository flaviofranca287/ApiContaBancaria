using DesafioStone.Data.Dtos;
using DesafioStone.Models;
using Microsoft.AspNetCore.Mvc;

// Quem verifica se tem dinheiro no domínio aqui.
namespace DesafioStone.Data
{
    public interface IAccountServices
    {
        CreateAccountResponse CreateAccount(CreateAccountRequest request);
        void DeleteAccount(DeleteAccountRequest accountDto);
        Account GetAccount(int id);
        void UpdateAccount(Account account);
    }
    // O meu objeto está implementando minha interface que é criar a conta e retornar uma response
    public class AccountServices : IAccountServices
    {
        private IUnitOfWork _unitOfWork;

        public AccountServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public CreateAccountResponse CreateAccount(CreateAccountRequest accountDto)
        {
            var account = _unitOfWork.AccountRepository.Add(new Models.Account(accountDto.OwnerOfAccount));
            var response = new CreateAccountResponse()
            {
                IdAccount = account.Id,
                OwnerOfAccount = account.OwnerOfAccount,
                Balance = account.Balance
            };
            return response;
        }
        public void DeleteAccount(DeleteAccountRequest accountDto)
        {
            _unitOfWork.AccountRepository.Remove(new Models.Account(accountDto.Id));
        }

        public Account GetAccount(int id)
        {
            return _unitOfWork.AccountRepository.Get(id);
        }

        public void UpdateAccount(Account account)
        {
            _unitOfWork.AccountRepository.Update(account);
        }
    }
}
