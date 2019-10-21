using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestTask.ViewModels.Shared;

namespace TestTask.ViewModels.Users
{
    public class EditVM : BaseEditVM<User>
    {
        [DisplayName("User Name :")]
        [Required(ErrorMessage = "This field is Required!")]
        public string UserName { get; set; }      


        [DisplayName("Password :")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Password { get; set; }


        [Display(Name = "Confirm Password :")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; }


        public bool IsAdmin { get; set; }


        public EditVM() { }

        public override void PopulateEntity(User item)
        {

            item.Id = Id;
            item.UserName = UserName;           
            item.Password = Password;
            item.IsAdmin = IsAdmin;
           
        }

        public override void PopulateModel(User item)
        {
            Id = item.Id;
            UserName = item.UserName;           
            Password = item.Password;
            IsAdmin = item.IsAdmin;
        }
    }
}