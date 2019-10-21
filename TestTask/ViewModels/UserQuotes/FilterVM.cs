using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using TestTask.Attributes.FilterVM;
using TestTask.Models;
using TestTask.ViewModels.Shared;

namespace TestTask.ViewModels.UserQuotes
{
    public class FilterVM : BaseFilterVM<DataAccess.Entity.UserQuote>
    {
        [Skip]
        public int? UserId { get; set; }

        [DropDownFilter(TargetModelProperty = "UserId", DataProperty = "Value", DisplayProperty = "Text")]
        public SelectList UsersList { get; set; }

        [Skip]
        public int? QuoteId { get; set; }

        [DropDownFilter(TargetModelProperty = "Score", DataProperty = "Value", DisplayProperty = "Text")]
        public SelectList ScoresList { get; set; }

        [Skip]
        public bool? Score { get; set; }


        public override Expression<Func<DataAccess.Entity.UserQuote, bool>> GenerateFilter()
        {
            return i => ((UserId == null) || i.UserId == UserId) &&
                        (QuoteId == null || i.QuoteId == QuoteId) &&
                        (Score == null || i.Score == Score);




        }
    }
}