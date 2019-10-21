using DataAccess.Entity;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TestTask.Attributes.FilterVM;
using TestTask.ViewModels.Shared;

namespace TestTask.ViewModels.Quotes
{
    public class EditVM : BaseEditVM<Quote>
    {

        [DisplayName("Text")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Text { get; set; }
       
        
        public int? AuthorId { get; set; }

        [DisplayName("Author")]
        [DropDownFilter(TargetModelProperty = "AuthorId", DataProperty = "Value", DisplayProperty = "Text")]
        public SelectList AuthorsList { get; set; }

        [Display(Name ="Add new Author : ")]
        public string NewAuthor { get; set; }

        public string AuthorName { get; set; }

        public List<UserQuote> userQuotes { get; set; }


        public override void PopulateEntity(Quote item)
        {
            item.Id = Id;
            item.Text = Text;            
            item.AuthorId = AuthorId!=null?(int)AuthorId:0;
            item.UserQuotes = userQuotes;
        }

        public override void PopulateModel(Quote item)
        {
            Id = item.Id;
            Text = item.Text;
            AuthorId = item.AuthorId;
            userQuotes = (List<UserQuote>)item.UserQuotes;
        }
    }
}