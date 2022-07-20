using DesafioStone.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioStone.Data
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> opt) : base(opt) //Recebo as opções do filme context e o construtor da classe já recebe e faz tudo pra gente
        {


        }
        // é o nosso conjunto de dados do banco que vamos conseguir fazer de maneira encapsulada o acesso aos dados do banco.
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }//Isso é uma entidade, necessariamente é uma tabela de dados.
    }
}
//Unit of work pesquisar