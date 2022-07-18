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
    public class ContaController : ControllerBase
    {
        private ContaContext _context;

        public ContaController(ContaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public void CreateAccount([FromBody] CreateAccountDto contaDto)
        {
            Conta conta = new Conta
            {
                Titular = contaDto.Titular,
                Saldo = contaDto.Saldo,
                
            };
            _context.Contas.Add(conta);
            _context.SaveChanges();
            Console.WriteLine(conta.Titular);
        }
        [HttpPut("{id}")]
        public IActionResult Deposit(int id,[FromBody] DepositDto valorSomadoDto)
        {
            Conta conta = _context.Contas.FirstOrDefault(conta => conta.Id == id);
            if (conta == null)
            {
                return NotFound();
            }
            conta.Saldo = valorSomadoDto.Saldo + conta.Saldo;
            _context.SaveChanges();
            return Ok(conta.Saldo);
        }
        [HttpGet]
        public IEnumerable<Conta> GetAccount()
        {
            return _context.Contas;
        }
    }
}
