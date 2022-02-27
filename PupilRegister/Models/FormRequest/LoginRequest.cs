using System.ComponentModel.DataAnnotations;

namespace PupilRegister.Models.FormRequest
{
    public class LoginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(12)]
        public string Password { get; set; }    
    }
}
