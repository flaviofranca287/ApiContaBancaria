﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioStone.Models
{
    //[Table("Transaction")]
    public class Transaction
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string TransactionType { get; set; } // a partir do momento que a tabela existe precisa de um construtor público para fazer essas amarrações, se eu deixo sem o getset eu não consigo criar na mão
        public string NameTransactor { get; set; }
        public string NameReceiver { get; set; }
        public DateTime TransactionDate { get; set; }
        public double TransactionValue { get; set; }
        public double IdTransactor { get; set; }
        public double IdReceiver { get; set; }
        
        //O meu transaction services vai receber uma solicitação de criação de histórico de transação, com os dados, o tipo de transação, etc.
        // Quem vai montar todo o caso de uso em cima da regra de negócio ´´e o transaction services
    }
}
