using DesafioStone.Data;
using DesafioStone.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioStone
{
    public class AccountRepository : IReposity<Account>
    {
        private AccountContext _context;
        private DbSet<Account> _accounts;

        public AccountRepository(AccountContext context)
        {
            _context = context;
            _accounts = _context.Set<Account>();
        }
    
        public void Add(Account entity)
        {
            _accounts.Add(entity);
            _context.SaveChanges();
        }
        //Preciso recuperar o contexto de conexao com o banco
    }
}
