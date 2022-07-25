using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioStone.Models
{
    //[Table("Account")] //Mostrando que é uma tabela para a pessoa não confundir (BP)
    public class Account
    {
        [Key]
        [Required]
        public int Id { get; set; }
        //[Column("")]
        public string OwnerOfAccount { get; set; }
        public double Balance { get; set; }

        public Account(string ownerOfAccount)
        {
            Balance = 0;
            OwnerOfAccount = ownerOfAccount;
        }
        public Account(int id)
        {
            Id = id;
        }
        public Account()
        {

        }
        public bool EuPossoSacar( double valor)
        {
            if ( Balance >= valor)
            {
                return true;
            }
            return false;
        }

    }
}
