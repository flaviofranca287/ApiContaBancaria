using DesafioStone.Data;
using DesafioStone.Data.Dtos;
using DesafioStone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DesafioStone.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AccountController : ControllerBase
    {
        
        private IAccountServices _accountServices;
        private ITransactionService _transactionService;
        public AccountController(IAccountServices services, ITransactionService transactionService)
        {
            _accountServices = services;
            _transactionService = transactionService;
        }
        [HttpPost]
        //CreateAccountDto pode ser create account request
        public ActionResult CreateAccount([FromBody] CreateAccountRequest createAccountRequest)
        {
            var response = _accountServices.CreateAccount(createAccountRequest);

            return StatusCode((int)HttpStatusCode.Created, response);

        }
        [HttpPost("deposito")]
        public IActionResult Deposit([FromBody] DepositRequest depositRequest)
        {
         
            var response = _transactionService.Deposit(depositRequest);
            return StatusCode((int)HttpStatusCode.OK, response);
        }
        [HttpPost("saque/{id}")]
        //clientself é uma request
        //public IActionResult BankDraft(int id, [FromBody] ClientSelfTransactionsDto alternatedValueDto)
        //{
        //    Models.Account account = Queryable.FirstOrDefault<Models.Account>(_context.Accounts, account => account.Id == id);
        //    if (account == null)
        //    {
        //        return NotFound();
        //    }
        //    if (account.Balance >= alternatedValueDto.ValueOfTransaction - 4)
        //    {
        //        double bankTax = 4.00;
        //        account.Balance -= alternatedValueDto.ValueOfTransaction - bankTax;
        //        _context.Transactions.Add(new Transaction()
        //        {
        //            TransactionDate = DateTime.Now,
        //            NameTransactor = account.OwnerOfAccount,
        //            NameReceiver = account.OwnerOfAccount,
        //            TransactionValue = alternatedValueDto.ValueOfTransaction,
        //            TransactionType = "Saque",
        //            IdTransactor = account.Id,
        //            IdReceiver = account.Id
        //        });
        //        _context.SaveChanges();
        //        //account.balance é um dado bruto, eu deveria passar um response trabalhado,o comprovante de saque tem que ter o que?? O valor que eu to sacando, a conta que ta saindo,a hora e o valor que ficou lá.
        //        return Ok(account.Balance);
        //    }
        //    return BadRequest();
        //}
        [HttpGet]
        public IEnumerable<Models.Account> GetAccounts()
        {
            return _context.Accounts;
        }
        [HttpGet("transactions")]
        public IEnumerable<Models.Transaction> GetTransactions()
        {

            return _context.Transactions;
        }
        [HttpGet("transactions/{idTransactorOrReceiver}")]
        public IActionResult GetTransactionById(int idTransactorOrReceiver)
        {
            //Models.Account account = Queryable.FirstOrDefault<Models.Account>(_context.Accounts, account => account.Id == id);
            //Models.Transaction transaction = (Transaction)Queryable.Where(_context.Transactions, q => q.IdTransactor == idTransactor);
            //é mais fácil usar o contexto que eu tenho pronto do accountcontext
            List<Models.Transaction> transactions = _context.Transactions.Where(transaction => (transaction.IdTransactor == idTransactorOrReceiver) || (transaction.IdReceiver == idTransactorOrReceiver)).ToList();

            var response = new List<TransactionExtractDto>();
            if (transactions.Any())
            {
                foreach (var transaction in transactions)
                {
                    response.Add(new TransactionExtractDto()
                    {
                        Id = transaction.Id,
                        IdTransactor = transaction.IdTransactor,
                        TransactionType = transaction.TransactionType,
                        TransactionValue = transaction.TransactionValue,
                        TransactionDate = transaction.TransactionDate,
                        IdReceiver = transaction.IdReceiver,
                        NameReceiver = transaction.NameReceiver,
                        NameTransactor = transaction.NameTransactor
                    });
                }
                return Ok(response);
            }
            return NotFound();
        }

        //[HttpGet("{id}")]
        //public IActionResult Extract(int id)
        //{
        //    Models.Account account = Queryable.FirstOrDefault<Models.Account>(_context.Accounts, account => account.Id == id);
        //    if (account != null)
        //    {
        //        ExtractDto extractDto = new ExtractDto
        //        {
        //            Id = account.Id,
        //            OwnerOfAccount = account.OwnerOfAccount,
        //            Balance = account.Balance,

        //        };
        //        return Ok(extractDto);
        //    }
        //    return NotFound();
        //}

        [HttpDelete]
        public ActionResult DeleteAccount([FromBody] DeleteAccountRequest deleteAccountRequest)
        {
            _accountServices.DeleteAccount(deleteAccountRequest);
            return StatusCode((int)HttpStatusCode.OK);
        }
        //[HttpPost("transaction")]
        //public IActionResult TransactionBetweenAccounts(int idTransactor, int idReceiver, [FromBody] TransactionDto transactionDto)
        //{
        //    Account accountTransactor = _context.Accounts.FirstOrDefault(accountTransactor => accountTransactor.Id == idTransactor);
        //    Account accountReceiver = _context.Accounts.FirstOrDefault(accountReceiver => accountReceiver.Id == idReceiver);
        //    if (accountReceiver == null || accountTransactor == null)
        //    {
        //        return NotFound();
        //    }
        //    accountTransactor.Balance = accountTransactor.Balance - transactionDto.TransactionValue;
        //    accountReceiver.Balance = accountReceiver.Balance + transactionDto.TransactionValue;
        //    _context.Transactions.Add(new Transaction()
        //    {
        //        TransactionDate = DateTime.Now,
        //        NameTransactor = accountTransactor.OwnerOfAccount,
        //        NameReceiver = accountReceiver.OwnerOfAccount,
        //        TransactionValue = transactionDto.TransactionValue,
        //        TransactionType = "Transferência Bancária",
        //        IdTransactor = accountTransactor.Id,
        //        IdReceiver = accountReceiver.Id
        //    });
        //    _context.SaveChanges();
        //    return NoContent();
        //}
    }
}
