using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace userdb.Models
{
    public class UserEditViewModel 
    {
        
        public UserInformationViewModel UserInfo { get; set; }
        public UserPasswordViewModel UserPassword { get; set; }
        
    }

}