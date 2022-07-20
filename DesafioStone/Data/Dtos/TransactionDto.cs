using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioStone.Data.Dtos
{
    public class TransactionDto
    {
        [Key]
        public int TransactionId { get; set; }
        [Required]
        public string TransactionType { get; set; }
        public string NameTransactor { get; set; }
        public string NameReceiver { get; set; }
        public DateTime TransactionDate { get; set; }
        public double TransactionValue { get; set; }

    }
}
