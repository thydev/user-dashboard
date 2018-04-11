using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Please input a valid level 1-9")]
        [Range(1, 9)]
        public int Level { get; set; }

        [Required(ErrorMessage = "Please input a valid e-mail address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password must be at least 8 characters long")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }
    }
}