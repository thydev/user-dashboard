using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace userdb.Models
{
    public class UserViewModel : BaseEntity
    {
        

        [Required(ErrorMessage = "First name must be at least 4 characters long")]
        [MinLength(4)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters")]
        [Display(Name="First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name must be at least 4 characters long")]
        [MinLength(4)]
        [Display(Name="Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please input a valid e-mail address")]
        [EmailAddress]
        // [EmailValidate(context)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password must be at least 4 characters long")]
        [DataType(DataType.Password)]
        [MinLength(4)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }

        
    }

    //??? Not working, need a database connection?
    public class EmailValidateAttribute : ValidationAttribute
        {
        private static UserDBContext _context;
    
        public EmailValidateAttribute(UserDBContext context)
        {
            _context = context;
        }
            public override bool IsValid(object value)
            {
                bool IsExist = _context.Users.Any(r => r.Email == (string)value);
                if ( IsExist)
                {
                    return false;
                }
                return true;
            }
        }
}