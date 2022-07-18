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
        [HttpPatch("{id}")]
        public IActionResult Deposit(int id, [FromBody] ClientSelfTransactionsDto alternatedValueDto)
        {
            Models.Account account = Queryable.FirstOrDefault<Models.Account>(_context.Accounts, account => account.Id == id);
            if (account == null)
            {
                return NotFound();
            }
            if (alternatedValueDto.Deposit == false && account.Balance >= alternatedValueDto.Balance)
            {
                double bankTax = 4.00;
                account.Balance -= alternatedValueDto.Balance;
                _context.SaveChanges();
                return Ok(account.Balance);
            }
            if (alternatedValueDto.Deposit == true)
            {
                //revisar
                double bankTax = alternatedValueDto.Balance * 0.01;
                double aux = alternatedValueDto.Balance - bankTax;
                account.Balance = aux + account.Balance;
                _context.SaveChanges();
                return Ok("Saldo da conta após o depósito: R$"+account.Balance);
            }
            return BadRequest();
        }
        [HttpGet]
        public IEnumerable<Models.Account> GetAccounts()
        {
            return _context.Accounts;
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
                    Balance = account.Balance
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
        //[HttpPost("{id}")]
        //public IActionResult CreateExtract(int id)
        //{
        //    var account = _context.Accounts.FirstOrDefault(account => account.Id == id);
        //    _context.Add(account);
        //}
    }
}
