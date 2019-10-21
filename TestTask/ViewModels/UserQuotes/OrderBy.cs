using System;
using System.Linq;
using TestTask.ViewModels.Shared;

namespace TestTask.ViewModels.UserQuotes
{
    public class OrderBy:BaseOrderBy<DataAccess.Entity.UserQuote>
    {
        public string UserName { get; set; }
        public string QuoteText { get; set; }
        public string Score { get; set; }
        public override Func<IQueryable<DataAccess.Entity.UserQuote>, IOrderedQueryable<DataAccess.Entity.UserQuote>> orderBy()
        {
            if (!string.IsNullOrEmpty(UserName))
            {
                return u => u.OrderBy(i => i.User.UserName);
            }
            else if (!string.IsNullOrEmpty(QuoteText))
            {
                return u => u.OrderBy(i => i.Quote.Text);
            }
            else if (!string.IsNullOrEmpty(Score))
            {
                return u => u.OrderBy(i => i.Score);
            }
            return null;
        }
    }
}