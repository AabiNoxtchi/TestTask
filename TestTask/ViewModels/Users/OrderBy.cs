using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestTask.ViewModels.Shared;

namespace TestTask.ViewModels.Users
{
    public class OrderBy : BaseOrderBy<User>
    {
        public string UserName { get; set; }
       
        public override Func<IQueryable<User>, IOrderedQueryable<User>> orderBy()
        {
            if (!string.IsNullOrEmpty(UserName))
            {
                return u => u.OrderBy(i => i.UserName);
            }
            return null;
        }
    }
}