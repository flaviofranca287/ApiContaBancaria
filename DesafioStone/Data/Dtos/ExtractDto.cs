using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioStone.Data.Dtos
{
    public class ExtractDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int TransactionId { get; set; }
        [Required]
        public string TransactionType;
        [Required]
        public DateTime TransactionDate;
        public string OwnerOfAccount { get; set; }
        public double Balance { get; set; }


    }
}
