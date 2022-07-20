using DesafioStone.Data;
using DesafioStone.Data.Dtos;
using DesafioStone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioStone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private Data.AccountContext _context;
        public AccountController(Data.AccountContext context)
        {
            _context = context;
        }

        [HttpPost]
        //CreateAccountDto pode ser create account request
        public IActionResult CreateAccount([FromBody] CreateAccountDto accountDto)
        {
            Models.Account account = new Models.Account
            {
                OwnerOfAccount = accountDto.OwnerOfAccount,
                Balance = accountDto.Balance,
            };

            _context.Accounts.Add(account);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Extract), new { id = account.Id }, account);
        }
        [HttpPost("deposito/{id}")]
        public IActionResult Deposit(int id, [FromBody] ClientSelfTransactionsDto alternatedValueDto)
        {
            Models.Account account = Queryable.FirstOrDefault<Models.Account>(_context.Accounts, account => account.Id == id);
            if (account == null)
            {
                return NotFound();
            }
            double bankTax = alternatedValueDto.Balance * 0.01;
            double aux = alternatedValueDto.Balance - bankTax;
            account.Balance = aux + account.Balance;
            _context.Transactions.Add(new Transaction()
            {
                TransactionDate = DateTime.Now,
                NameTransactor = account.OwnerOfAccount,
                NameReceiver = account.OwnerOfAccount,
                TransactionValue = alternatedValueDto.Balance,
                TransactionType = "Depósito",
                IdTransactor = account.Id
            });
            _context.SaveChanges();
            return Ok("Saldo da conta após o depósito: R$" + account.Balance);

        }
        [HttpPost("saque/{id}")]
        //clientself é uma request
        public IActionResult BankDraft(int id, [FromBody] ClientSelfTransactionsDto alternatedValueDto)
        {
            Models.Account account = Queryable.FirstOrDefault<Models.Account>(_context.Accounts, account => account.Id == id);
            if (account == null)
            {
                return NotFound();
            }
            if (account.Balance >= alternatedValueDto.Balance - 4)
            {
                double bankTax = 4.00;
                account.Balance -= alternatedValueDto.Balance - bankTax;
                _context.Transactions.Add(new Transaction()
                {
                    TransactionDate = DateTime.Now,
                    NameTransactor = account.OwnerOfAccount,
                    NameReceiver = account.OwnerOfAccount,
                    TransactionValue = alternatedValueDto.Balance,
                    TransactionType = "Saque",
                    IdTransactor = account.Id

                });
                _context.SaveChanges(); 
                //account.balance é um dado bruto, eu deveria passar um response trabalhado,o comprovante de saque tem que ter o que?? O valor que eu to sacando, a conta que ta saindo,a hora e o valor que ficou lá.
                return Ok(account.Balance);
            }
            return BadRequest();
        }
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
        [HttpGet("transactions/{idTransactor}")]
        public IActionResult GetTransactionById(int idTransactor)
        {
            //Models.Account account = Queryable.FirstOrDefault<Models.Account>(_context.Accounts, account => account.Id == id);
            //Models.Transaction transaction = (Transaction)Queryable.Where(_context.Transactions, q => q.IdTransactor == idTransactor);
            //é mais fácil usar o contexto que eu tenho pronto do accountcontext
            List<Models.Transaction> transactions = _context.Transactions.Where(transaction => transaction.IdTransactor == idTransactor).ToList();

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
                    });
                }
                return Ok(response);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Extract(int id)
        {
            Models.Account account = Queryable.FirstOrDefault<Models.Account>(_context.Accounts, account => account.Id == id);
            if (account != null)
            {
                ExtractDto extractDto = new ExtractDto
                {
                    Id = account.Id,
                    OwnerOfAccount = account.OwnerOfAccount,
                    Balance = account.Balance,

                };
                return Ok(extractDto);
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            Account account = _context.Accounts.FirstOrDefault(account => account.Id == id);
            if (account == null)
            {
                return NotFound();
            }
            _context.Remove(account);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPost("transaction")]
        public IActionResult TransactionBetweenAccounts(int idTransactor, int idReceiver, [FromBody] TransactionDto transactionDto)
        {
            Account accountTransactor = _context.Accounts.FirstOrDefault(accountTransactor => accountTransactor.Id == idTransactor);
            Account accountReceiver = _context.Accounts.FirstOrDefault(accountReceiver => accountReceiver.Id == idReceiver);
            if (accountReceiver == null || accountTransactor == null)
            {
                return NotFound();
            }
            accountTransactor.Balance = accountTransactor.Balance - transactionDto.TransactionValue;
            accountReceiver.Balance = accountReceiver.Balance + transactionDto.TransactionValue;
            _context.Transactions.Add(new Transaction()
            {
                TransactionDate = DateTime.Now,
                NameTransactor = accountTransactor.OwnerOfAccount,
                NameReceiver = accountReceiver.OwnerOfAccount,
                TransactionValue = transactionDto.TransactionValue,
                TransactionType = "Transferência Bancária",
                IdTransactor = accountTransactor.Id,
            });
            _context.SaveChanges();
            return NoContent();
        }
    }
}
