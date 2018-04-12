using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace userdb.Models
{
    public class UserPasswordViewModel : BaseEntity
    {
        
        public int UserId { get; set; }
        
        [Required(ErrorMessage = "Password must be at least 4 characters long")]
        [DataType(DataType.Password)]
        [MinLength(4)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        [Display(Name="Confirm")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }
        
    }

}