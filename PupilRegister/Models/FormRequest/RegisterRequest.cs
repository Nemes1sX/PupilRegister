using System.ComponentModel.DataAnnotations;

namespace PupilRegister.Models.FormRequest
{
    public class RegisterRequest
    {
        [Required, MinLength(6)]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(12)]
        public string Password { get; set; }
    }
}
