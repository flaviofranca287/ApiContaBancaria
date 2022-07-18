using System.ComponentModel.DataAnnotations;

namespace DesafioStone.Data.Dtos
{
    public class ClientSelfTransactionsDto
    {
        [StringLength(30, ErrorMessage = "O nome do titular não pode passar de 30 caracteres")]
        public string Titular { get; set; }
        public double Saldo { get; set; }
        public bool Deposit { get; set; }
    }
}
