namespace DesafioStone.Data
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; }
        ITransactionRepository TransactionRepository { get; }
    }
    //Esse cara que vai gerir todos os repositórios da minha modelagem
    //Ele é um orquestrador de repositórios, logo preciso expor as camadas
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AccountContext _accountContext;
        private IAccountRepository _accountRepository;
        private ITransactionRepository _transactionRepository;
        public UnitOfWork(AccountContext accountContext)
        {
            _accountContext = accountContext;
   
        }
        //o ?? checa se for nulo, caso não seja nulo ele retorna ele, se for nulo, ele cria um novo accountrepository
        public IAccountRepository AccountRepository => _accountRepository ?? (_accountRepository = new AccountRepository(_accountContext));
        public ITransactionRepository TransactionRepository => _transactionRepository ?? (_transactionRepository = new TransactionRepository(_accountContext));
    }
}