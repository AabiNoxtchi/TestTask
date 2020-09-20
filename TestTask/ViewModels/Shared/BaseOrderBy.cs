using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTask.ViewModels.Shared
{
    public class BaseOrderBy<E>
    {
        // public string orderBY { get; set; }
        public string Prefix { get; set; }

        public virtual Func<IQueryable<E>, IOrderedQueryable<E>> orderBy() { return null; }
    }
}