using DesafioStone.Data;
using DesafioStone.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DesafioStone
{
    public interface IAccountRepository : IRepository<Account>
    {

    }
    /*Observação legal: quando eu aplico um comportamento de interface na minha classe,
     ela obrigatoriamente tem que ter todos os metodos da minha interface que estou estendendo.*/


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
        public Account Remove(Account account)
        {
            _accounts.Remove(account);
            _context.SaveChanges();
            return account;
        }

        //Preciso recuperar o contexto de conexao com o banco
    }
}
