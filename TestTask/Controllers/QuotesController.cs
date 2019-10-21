using DataAccess.Entity;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using TestTask.Filters;
using TestTask.Models;
using TestTask.Services;
using TestTask.ViewModels.Quotes;
using TestTask.ViewModels.Shared;

namespace TestTask.Controllers
{
   
    public class QuotesController : BaseController<Quote, QuotesRepository, FilterVM, IndexVM, EditVM,OrderBy>
    {


       
        private List<SelectListItem> PopulateSelectList()
        {
            AuthorsRepository repo = new AuthorsRepository();
            List<Author> dbAuthors = repo.GetAll(a => true);

            List<SelectListItem> listItems = new List<SelectListItem>();

            foreach (Author author in dbAuthors)
            {
                listItems.Add(new SelectListItem()
                {
                    Text = author.Name,
                    Value = author.Id.ToString()

                });
            }
            return listItems;
        }

        protected override void CheckModel(EditVM model, ref bool valid) {

            if ((model.AuthorId != null && model.NewAuthor != null) || (model.AuthorId == null && model.NewAuthor == null))
            {
                valid = false;
                PopulateEditGetModel(model);
                ModelState.AddModelError("", "You have to choose either an existing author from the list or add anew one !");

            }
        }

        protected override void PopulateModel(IndexVM model)
        {
            model.ChildElement = "Author";
            List<SelectListItem> AuthorslistItems = PopulateSelectList();
            model.Filter.AuthorsList = new SelectList(AuthorslistItems, "Value", "Text", null);
        }

        protected override void PopulateEditGetModel(EditVM model)
        {

            List<SelectListItem> AuthorslistItems = PopulateSelectList();

            model.AuthorsList = new SelectList(AuthorslistItems, "Value", "Text",model.AuthorId!=null?model.AuthorId.ToString():null);
        }

        protected override void PopulateEditPostModel(EditVM model)
        {
           
                if (!string.IsNullOrEmpty(model.NewAuthor))
                {
                    AuthorsRepository aRepo = new AuthorsRepository();
                    Author author = aRepo.GetAll(a => a.Name == model.NewAuthor).FirstOrDefault();
                    if (author != null)
                    {
                        model.AuthorId = author.Id;
                    }
                    else
                    {
                        author = new Author();
                        author.Name = model.NewAuthor;
                        aRepo.Save(author);
                        model.AuthorId = aRepo.GetAll(a => a.Name == model.NewAuthor).FirstOrDefault().Id;
                    }
                }
        }

        protected override Quote GetItem(int id, QuotesRepository repo, string childEntity) { return repo.GetById(id,"Author"); }

        protected override void CheckEditedChildEntities(EditVM model, Quote quote) {

            if (quote.AuthorId == model.AuthorId) return;

            UserQuotesRepository repo = new UserQuotesRepository();

            Expression<Func<UserQuote, bool>> filter = uq=>uq.QuoteId == quote.Id;

            model.userQuotes = model.userQuotes ?? new List<UserQuote>();
            model.userQuotes=repo.GetAll(filter);

            string authorCorrectAnswer = model.AuthorName;

            foreach (UserQuote item in model.userQuotes)
            {
                if (item.CorrectAuthorName == null) continue;

                item.CorrectAuthorName = authorCorrectAnswer;
                item.Score = UserQuotesController.CheckAnswer(item.SelectedAnswer,item.CorrectAuthorName,item.ShownAnswer);
            }

        }

        protected override void CheckIfIsDeleted(Quote item) {

           
        }


       /* [AuthorizeAdmin]
        public ActionResult RedirectAdmin(IndexVM model)
        {

            base.Index(model);
            return View("Index", model);

        }


        [HttpGet]
        [AllowAll]
        public override ActionResult Index(IndexVM model)
        {
            if (AuthenticationManager.LoggedUser != null && AuthenticationManager.LoggedUser.IsAdmin)
            {
                model.ChildElement = "Author";
                return RedirectToAction("RedirectAdmin", model);
            }
            else
            {
                return RedirectToAction("Play", "Play", model);
            }
            
        }*/





      

    }
}