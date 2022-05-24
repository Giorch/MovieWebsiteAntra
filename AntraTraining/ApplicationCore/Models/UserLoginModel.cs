using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Field required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field required")]
        public string Password  { get; set; }
    }
}
