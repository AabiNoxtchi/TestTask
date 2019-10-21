using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestTask.ViewModels.Shared;

namespace TestTask.ViewModels.UserQuotes
{
    public class PlayVM
    {
       
        public PagerVM Pager { get; set; }

        public Quote quote { get; set; }

        public List<Author> authors { get; set; }
       
        public string ChildElement { get; set; }

        public UserQuotes.EditVM UserQuotesEditVM { get; set; }

        public string msg { get; set; }


    }
}