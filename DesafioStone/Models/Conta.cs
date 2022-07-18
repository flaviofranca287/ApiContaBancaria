using System.ComponentModel.DataAnnotations;

namespace DesafioStone.Models
{
    public class Conta
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [StringLength(30, ErrorMessage = "O nome do titular não pode passar de 30 caracteres")]
        public string Titular { get; set; }
        public double Saldo { get; set; }
        //private double saldo;
        //public double Saldo {
        //    get
        //    {
        //        return saldo;
        //    }
        //    set
        //    {
        //        if (value < 0)
        //        {
        //            return;
        //        }
        //        saldo = value;
        //    }
        //}

    }
}
