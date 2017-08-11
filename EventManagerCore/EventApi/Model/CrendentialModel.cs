using System.ComponentModel.DataAnnotations;

namespace EventApi.Model
{
    public class CrendentialModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
