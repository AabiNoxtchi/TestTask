using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestTask.ViewModels.Home
{
    public class LoginVM
    {
        [DisplayName("User Name :")]
        [Required(ErrorMessage = "This field is Required!")]
        public string UserName { get; set; }

        [DisplayName("Password :")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Password { get; set; }
    }
}