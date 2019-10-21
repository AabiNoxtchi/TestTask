using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using TestTask.Attributes.FilterVM;
using TestTask.ViewModels.Shared;

namespace TestTask.ViewModels.Quotes
{
    public class FilterVM : BaseFilterVM<Quote>
    {
      
        public string Text { get; set; }

        [Skip]
        public Nullable<int> AuthorId { get; set; }

        [DisplayName("Author")]
        [DropDownFilter(TargetModelProperty = "AuthorId", DataProperty = "Value", DisplayProperty = "Text")]
        public SelectList AuthorsList { get; set; }

        public override Expression<Func<Quote, bool>> GenerateFilter()
        {
            return i => ((string.IsNullOrEmpty(Text) || i.Text == Text)) &&
            ((AuthorId==null)||i.AuthorId==AuthorId);
                       


        }


    }
}