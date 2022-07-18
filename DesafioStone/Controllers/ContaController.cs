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
        public IActionResult CreateAccount([FromBody] CreateAccountDto contaDto)
        {
            Conta conta = new Conta
            {
                Titular = contaDto.Titular,
                Saldo = contaDto.Saldo,

            };
            _context.Contas.Add(conta);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Extract), new { id = conta.Id }, conta);
        }
        [HttpPatch("{id}")]
        public IActionResult Deposit(int id, [FromBody] ClientSelfTransactionsDto alternatedValueDto)
        {
            Conta conta = _context.Contas.FirstOrDefault(conta => conta.Id == id);
            if (conta == null)
            {
                return NotFound();
            }
            if (alternatedValueDto.Deposit == false && conta.Saldo >= alternatedValueDto.Saldo)
            {
                conta.Saldo -= alternatedValueDto.Saldo;
                _context.SaveChanges();
                return Ok(conta.Saldo);
            }
            if (alternatedValueDto.Deposit == true)
            {
                conta.Saldo = alternatedValueDto.Saldo * 0.99 + conta.Saldo;
                _context.SaveChanges();
                return Ok(conta.Saldo);
            }
            return BadRequest();
        }
        [HttpGet]
        public IEnumerable<Conta> GetAccounts()
        {
            return _context.Contas;
        }
        [HttpGet("{id}")]
        public IActionResult Extract(int id)
        {
            Conta conta = _context.Contas.FirstOrDefault(conta => conta.Id == id);
            if (conta != null)
            {
                ExtractDto extractDto = new ExtractDto
                {
                    Id = conta.Id,
                    Titular = conta.Titular,
                    Saldo = conta.Saldo
                };
                return Ok(extractDto);
            }
            return NotFound();
        }
    }
}
