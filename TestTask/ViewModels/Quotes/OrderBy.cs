using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestTask.ViewModels.Shared;

namespace TestTask.ViewModels.Quotes
{

    public class OrderBy : BaseOrderBy<Quote>
    {
        public string Text { get; set; }
        public string AuthorName { get; set; }

        public override Func<IQueryable<Quote>, IOrderedQueryable<Quote>> orderBy()
        {
            if (!string.IsNullOrEmpty(Text))
            {
                return u => u.OrderBy(i => i.Text);
            }
            else if (!string.IsNullOrEmpty(AuthorName))
            {
                return u => u.OrderBy(i => i.Author.Name);
            }

            return null;
        }
    }
}