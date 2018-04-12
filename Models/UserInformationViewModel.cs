using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace userdb.Models
{
    public class UserInformationViewModel : BaseEntity
    {
        
        public int UserId { get; set; }

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

        public int Level { get; set; }
        
    }

}