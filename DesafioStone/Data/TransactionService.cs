using DesafioStone.Data.Dtos;
using System.Linq;

namespace DesafioStone.Data
{
    public interface ITransactionService
    {
        DepositResponse Deposit(DepositRequest request);
        BankDraftResponse BankDraft(BankDraftRequest request);
    }
    public class TransactionService : ITransactionService
    {
        private IUnitOfWork _unitOfWork;
        private IAccountServices _accountServices;
        public TransactionService(IUnitOfWork unitOfWork, IAccountServices accountServices)
        {
            _unitOfWork = unitOfWork;
            _accountServices = accountServices;
        }
        public DepositResponse Deposit(DepositRequest request)
        {
            
            var response = new DepositResponse();
            var account = _accountServices.GetAccount(request.Id);

            if (account != null)
            {
                double bankTax = request.ValueOfTransaction * 0.01;
                double aux = request.ValueOfTransaction - bankTax;
                account.Balance = aux + account.Balance;

                _unitOfWork.TransactionRepository.Add(new Models.Transaction()
                {
                    IdReceiver = account.Id,
                    IdTransactor = account.Id,
                    TransactionType = "Deposito",
                    TransactionDate = System.DateTime.Now,
                    TransactionValue = request.ValueOfTransaction
                });

                _accountServices.UpdateAccount(account);

                response = new DepositResponse()
                {
                    IdReceiver = account.Id,
                    IdTransactor = account.Id,
                    DateOfTransaction = System.DateTime.Now,
                    TransactionType = "Deposito",
                    ValueOfTransaction = request.ValueOfTransaction,
                    ActualBalance = account.Balance,
                    NameReceiver = account.OwnerOfAccount,
                    NameTransactor = account.OwnerOfAccount
                };
            }
            return response;
        }

        public BankDraftResponse BankDraft(BankDraftRequest request)
        {
            var response = new BankDraftResponse();
            var account = _accountServices.GetAccount(request.Id);

            if (account != null && account.Balance >= request.ValueOfTransaction -4)
            {
                double bankTax = 4.00;
                account.Balance -= request.ValueOfTransaction - bankTax;
                _unitOfWork.TransactionRepository.Add(new Models.Transaction()
                {
                    IdReceiver = account.Id,
                    IdTransactor = account.Id,
                    TransactionType = "Saque",
                    TransactionDate = System.DateTime.Now,
                    TransactionValue = request.ValueOfTransaction
                });
                response = new BankDraftResponse()
                {
                    IdReceiver = account.Id,
                    IdTransactor = account.Id,
                    DateOfTransaction = System.DateTime.Now,
                    TransactionType = "Saque",
                    ValueOfTransaction = request.ValueOfTransaction,
                    ActualBalance = account.Balance,
                    NameReceiver = account.OwnerOfAccount,
                    NameTransactor = account.OwnerOfAccount
                };
            }
            return response;
        }
    }
}
