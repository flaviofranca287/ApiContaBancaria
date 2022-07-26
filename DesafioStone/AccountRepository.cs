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
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(AccountContext context) : base(context) // to extendendo uma classe abstrata que eu vou herdar a injeção de context dela
        {
            
        }
    }
}
//Preciso ter uma camada que saiba instânciar meus objetos
//Injeção de dependencia é jogar para uma classe ou método um serviço que ela vai depender para utilizar.
/*Se eu chegar no meu account service e falar: vou fazer um depósito, quando eu fizer  o dep´´osito eu preciso
 salvar o histórico que ele ocorreu, então eu vou depender de um serviço que salva o histórico(que é o transaction)

A injeção de dependencia reduz a quantidade de codigo qeu eu escrevo e melhora significamente a forma como escrevo testes
ela recebe uma interface, um contrato de dados que alguem em algum plano implementa aquilo */