using System.ComponentModel.DataAnnotations;

namespace DesafioStone.Data.Dtos
{
    public class CreateAccountRequest
    {
        [Required]
        [StringLength(30, ErrorMessage = "O nome do titular não pode passar de 30 caracteres")]
        public string OwnerOfAccount { get; set; }
    }


}
