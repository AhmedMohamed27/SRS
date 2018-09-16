using SRS.Models.ValAttr;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRS.Models.ViewModel
{

    
    public class LoginVM
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [LoginCheck_AspUser]
        public string Error { get; set; }
    }
}