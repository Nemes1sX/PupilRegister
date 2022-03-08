using PupilRegister.DataContext;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PupilRegister.Models.FormRequest.Rules
{
    public class UniqueEmail : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var registerRequest = (RegisterRequest)validationContext.ObjectInstance;
            var _db = (PupilRegisterContext)validationContext.GetService(typeof(PupilRegisterContext));

            var user = _db.Parents.Any(x => x.Email == registerRequest.Email);

            return !user 
                ? ValidationResult.Success 
                : new ValidationResult("Email is already taken");
        }
    }
}
