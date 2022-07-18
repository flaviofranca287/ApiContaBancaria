using DesafioStone.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioStone.Data
{
    public class ContaContext : DbContext
    {
        public ContaContext(DbContextOptions<ContaContext> opt) : base(opt) //Recebo as opções do filme context e o construtor da classe já recebe e faz tudo pra gente
        {


        }
        // é o nosso conjunto de dados do banco que vamos conseguir fazer de maneira encapsulada o acesso aos dados do banco.
        public DbSet<Conta> Contas { get; set; }
    }
}
