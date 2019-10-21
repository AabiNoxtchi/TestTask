using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestTask.ViewModels.Shared;

namespace TestTask.ViewModels.Quotes
{
   
        public class IndexVM : BaseIndexVM<Quote, FilterVM, OrderBy>
        {
           public List<Author> authors { get; set; }
           public UserQuotes.EditVM UserQuotesEditVM { get; set; }
          

        }
   
}