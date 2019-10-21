using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestTask.ViewModels.Shared;

namespace TestTask.ViewModels.UserQuotes
{
    public class EditVM : BaseEditVM<UserQuote>
    {

        public int UserId { get; set; }

        public int QuoteId { get; set; }

        public bool Score { get; set; }

        public string SelectedAnswer { get; set; }

        public string ShownAnswer { get; set; }

        public string CorrectAuthorName { get; set; }


        public override void PopulateEntity(UserQuote item)
        {
            item.Id = Id;
            item.UserId = UserId;
            item.QuoteId = QuoteId;
            item.Score = Score;
            item.SelectedAnswer = SelectedAnswer;
            item.ShownAnswer = ShownAnswer;
            item.CorrectAuthorName = CorrectAuthorName;
        }

        public override void PopulateModel(UserQuote item)
        {
            Id = item.Id;
            UserId = item.UserId;
            QuoteId = item.QuoteId;
            Score = item.Score;
            SelectedAnswer = item.SelectedAnswer;
            ShownAnswer = item.ShownAnswer;
            CorrectAuthorName = item.CorrectAuthorName;
        }
    }
}