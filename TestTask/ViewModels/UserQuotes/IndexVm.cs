using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTask.ViewModels.Quotes;
using TestTask.ViewModels.Shared;

namespace TestTask.ViewModels.UserQuotes
{
    public class IndexVM : BaseIndexVM<UserQuote, FilterVM,OrderBy>
    {
        //public User user { get; set; }
       

    }
}