using System.ComponentModel.DataAnnotations;

namespace DataTransfer
{
    public class CredentialsDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
