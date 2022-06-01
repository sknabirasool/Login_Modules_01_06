using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LoginModule.Models
{
    public class Register
    {
       

           
            

            [Required(ErrorMessage = "Please enter your user Name")]
            [Display(Name = "User Name")]
            public string user_name { get; set; }
       

            [Required(ErrorMessage = "Please enter password")]
            [DataType(DataType.Password)]
            [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} character", MinimumLength = 8)]
            [RegularExpression(@"^([a-zA-Z0-9@*#]{8,15})$", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase      Alphabet, 1 Number and 1 Special Character")]
            public string password { get; set; }

           
            [Required]
            [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail id is not valid")]
            public string email { get; set; }

            

            public List<Register> registerinfo { get; set; }

        
    }
}
