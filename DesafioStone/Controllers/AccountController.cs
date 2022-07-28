using DesafioStone.Data;
using DesafioStone.Data.Dtos;
using DesafioStone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using System.Net;


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
        public ActionResult CreateAccount([FromBody] CreateAccountRequest createAccountRequest)
        {
            var response = _accountServices.CreateAccount(createAccountRequest);

            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpPost("deposito")]
        public ActionResult Deposit([FromBody] DepositRequest depositRequest)
        {
            var response = _transactionService.Deposit(depositRequest);
            return StatusCode((int)HttpStatusCode.OK, response);
        }

        [HttpPost("saque")]
        public ActionResult BankDraft([FromBody] BankDraftRequest bankDraftRequest)
        {
            var response = _transactionService.BankDraft((bankDraftRequest));
            return StatusCode((int)HttpStatusCode.OK, response);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> GetAccounts()
        {
            return Ok(_accountServices.GetAccounts());
        }

        [HttpDelete]
        public ActionResult DeleteAccount([FromBody] DeleteAccountRequest deleteAccountRequest)
        {
            _accountServices.DeleteAccount(deleteAccountRequest);
            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpPost("transaction")]
        public ActionResult TransactionBetweenAccounts(
            [FromBody] TransactionBetweenAccountsRequest transactionBetweenAccountsRequest)
        {
            var response = _transactionService.TransactionBetweenAccounts(transactionBetweenAccountsRequest);
            return StatusCode((int)HttpStatusCode.OK, response);
        }

        [HttpGet("transaction/{id}")]
        public ActionResult GetTransactionsById([FromRoute] int id)
        {
            var response = _transactionService.GetTransactionsById(id);
            return Ok(response);
        }
    }
}

// [HttpGet("transactions")]
// public IEnumerable<Models.Transaction> GetTransactions()
// {
//     return _context.Transactions;
// }

//Models.Account account = Queryable.FirstOrDefault<Models.Account>(_context.Accounts, account => account.Id == id);
//Models.Transaction transaction = (Transaction)Queryable.Where(_context.Transactions, q => q.IdTransactor == idTransactor);
//é mais fácil usar o contexto que eu tenho pronto do accountcontext