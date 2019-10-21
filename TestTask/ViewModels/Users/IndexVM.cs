using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestTask.ViewModels.Shared;

namespace TestTask.ViewModels.Users
{
    public class IndexVM : BaseIndexVM<User, FilterVM, OrderBy>
    {
        public UserQuotes.IndexVM UserQuotesIndexVM { get; set; }
       
    }
}