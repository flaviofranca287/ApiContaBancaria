using DesafioStone.Data;
using DesafioStone.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DesafioStone
{
    public interface ITransactionRepository : IRepository<Transaction>
    {

    };
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {

        public TransactionRepository(AccountContext context) : base(context)
        {

        }
    }
}
