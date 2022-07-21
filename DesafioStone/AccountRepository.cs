using DesafioStone.Data;
using DesafioStone.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioStone
{
    public interface IAccountRepository : IRepository<Account>
    {

    }
    public class AccountRepository : IAccountRepository
    {
        private AccountContext _context;
        private DbSet<Account> _accounts;

        public AccountRepository(AccountContext context)
        {
            _context = context;
            _accounts = _context.Set<Account>();
        }

        public Account Add(Account account)
        {
            _accounts.Add(account);
            _context.SaveChanges();
            return account;
        }
        //Preciso recuperar o contexto de conexao com o banco
    }
}
