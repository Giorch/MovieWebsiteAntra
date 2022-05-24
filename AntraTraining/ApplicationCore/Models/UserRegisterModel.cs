using ApplicationCore.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage ="Email cannot be empty")]
        [EmailAddress(ErrorMessage = "Email address must be in the right format")]
        [StringLength(100)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Field required")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Password field required"), DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z](?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$")]
        public string Password { get; set; }
        [Required,DataType(DataType.Date)]
        [YearValidation(1900)]
        public DateTime DateOfBirth { get; set; }
    }
}
