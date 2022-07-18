using System.ComponentModel.DataAnnotations;

namespace DesafioStone.Models
{
    public class Account
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [StringLength(30, ErrorMessage = "O nome do OwnerOfAccount não pode passar de 30 caracteres")]
        public string OwnerOfAccount { get; set; }
        public double Balance { get; set; }
        //private double Balance;
        //public double Balance {
        //    get
        //    {
        //        return Balance;
        //    }
        //    set
        //    {
        //        if (value < 0)
        //        {
        //            return;
        //        }
        //        Balance = value;
        //    }
        //}

    }
}
