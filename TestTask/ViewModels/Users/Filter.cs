using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TestTask.ViewModels.Shared;

namespace TestTask.ViewModels.Users
{
    public class FilterVM : BaseFilterVM<User>
    {

        
        [DisplayName("User Name:")]
        public string UserName { get; set; }


        public override Expression<Func<User, bool>> GenerateFilter()
        {
            return i => 
                        (
                        string.IsNullOrEmpty(UserName) || i.UserName.Contains(UserName)
                        );
        }
    }
}