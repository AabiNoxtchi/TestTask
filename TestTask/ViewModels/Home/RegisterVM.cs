using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestTask.ViewModels.Home
{
    public class RegisterVM
    {
        [Required]
        [Display(Name ="User Name")]
        public string UserName { get; set; }      
       
        [Required]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; }

       



        public RegisterVM() { }

        public void PopulateEntity(User item)
        {

            item.UserName = UserName;
            item.Password = Password;
           
        }


    }
}