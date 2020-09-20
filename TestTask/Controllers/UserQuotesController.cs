using DataAccess.Entity;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using TestTask.Models;
using TestTask.ViewModels.UserQuotes;

namespace TestTask.Controllers
{
    public class UserQuotesController : BaseController<UserQuote, UserQuotesRepository, FilterVM, IndexVM, EditVM, OrderBy>
    {
        private List<SelectListItem> PopulateFilterLists()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem()
            {
                Text = "",
                Value = null

            });
            return listItems;
        }

        private void PopulateScoreList(IndexVM model)
        {
            List<SelectListItem> ScoreItems = PopulateFilterLists();
            ScoreItems.AddRange(new List<SelectListItem>()
            {
                new SelectListItem()
            {
                Text = true.ToString(),
                Value = true.ToString()
            } ,
            new SelectListItem()
            {
                Text = false.ToString(),
                Value = false.ToString()
            }

            });

            model.Filter.ScoresList = new SelectList(ScoreItems, "Value", "Text", null);
        }

        private void PopulateUserList(IndexVM model)
        {
            UsersRepository urepo = new UsersRepository();
            List<User> listUsers = urepo.GetAll(null);

            List<SelectListItem> listItems = PopulateFilterLists();

            foreach (User user in listUsers)
            {
                listItems.Add(new SelectListItem()
                {
                    Text = user.UserName,
                    Value = user.Id.ToString()

                });
            }

            model.Filter.UsersList = new SelectList(listItems, "Value", "Text", model.Filter.UserId);
        }

        private static void Shuffle(IList<Author> list)
        {
            Random rng = new Random(); 
            int n = list.Count - 1;
            int k = rng.Next(n + 1);
            Author value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        private void SaveUserQuote(PlayVM model, Quote quote)
        {
            UserQuotesRepository uqRepo = new UserQuotesRepository();
            UserQuote item = new UserQuote();

            model.UserQuotesEditVM.UserId = AuthenticationManager.LoggedUser.Id;
            model.UserQuotesEditVM.QuoteId = quote.Id;
            model.UserQuotesEditVM.CorrectAuthorName = quote.Author.Name;

            if (model.UserQuotesEditVM.SelectedAnswer == "No" || model.UserQuotesEditVM.SelectedAnswer == "Yes")
                model.UserQuotesEditVM.ShownAnswer = model.authors[0].Name;
            else
                model.UserQuotesEditVM.ShownAnswer = "Options";

            model.UserQuotesEditVM.PopulateEntity(item);
            uqRepo.Save(item);
        }

        private string GenerateMsg(bool result, string name)
        {

            if (result)
                return "Correct! The right answer is: " + name;
            else
                return "Sorry, you are wrong! The right answer is: " + name;
        }

        private List<Author> PopulateAuthorList(List<Author> authors, Quote quote)
        {
            AuthorsRepository aRepo = new AuthorsRepository();
            int authorUnwantedId = quote.Author.Id;
            Expression<Func<Author, bool>> authorFilter = a => a.Id != authorUnwantedId;

            Func<IQueryable<Author>, IOrderedQueryable<Author>> order = u => u.OrderBy(r => Guid.NewGuid());

            authors = aRepo.GetAll(authorFilter, 1, 2, order);

            authors.Add(quote.Author);
            Shuffle(authors);

            return authors;

        }

        private void KeepTrackOfPlayedGames(Quote quote)
        {
            List<int> playedGames;

            if (Session["PlayedGames"] == null)
            {
                playedGames = new List<int>();
            }
            else
                playedGames = (List<int>)Session["PlayedGames"];

            playedGames.Add(quote.Id);
            Session["PlayedGames"] = playedGames;
        }

        private Quote ChooseQuote()
        {
            QuotesRepository qRepo = new QuotesRepository();
            int count = qRepo.Count(null);
            Expression<Func<Quote, bool>> Filter = null;

            if (Session["PlayedGames"] != null)
            {

                List<int> PlayedGames = (List<int>)Session["PlayedGames"];

                if (PlayedGames.Count >= count) return null;

                Filter = x => !PlayedGames.Contains(x.Id);

            }
            Func<IQueryable<Quote>, IOrderedQueryable<Quote>> order = u => u.OrderBy(r => Guid.NewGuid());
            return qRepo.GetAll(Filter, 1, 1, order, "Author").FirstOrDefault();
        }

        internal static bool CheckAnswer(string selectedAnswer, string AuthorName, string AuthorsFirstOption)
        {
            bool correctAnswer;

            if (selectedAnswer == "Yes")
            {
                correctAnswer = AuthorsFirstOption == AuthorName;
            }
            else if (selectedAnswer == "No")
            {
                correctAnswer = AuthorsFirstOption != AuthorName;
            }
            else
            {
                correctAnswer = selectedAnswer == AuthorName;
            }

            return correctAnswer;
        }


        protected override void PopulateModel(IndexVM model)
        {
            PopulateUserList(model);
            PopulateScoreList(model);
           
            //////////////////////////////////////////???????????????????????????????????????????
            model.ChildElement = "Quote";
      
    }


        [HttpGet]
        public ActionResult Play(PlayVM model)
        {

            if (Session["GameMode"] == null)
            {
                Session["GameMode"] = "Binary";
            }

            model.quote = ChooseQuote();

            if (model.quote == null) { return View("TheEnd"); }
            model.msg = "";

            model.authors = PopulateAuthorList(model.authors, model.quote);
            model.quote.Author = null;

            return View(model);
        }

        //[HttpPost]
        public ActionResult EditChildEntity(PlayVM model)
        {
            QuotesRepository qRepo = new QuotesRepository();
            Quote quote = qRepo.GetById(model.quote.Id, "Author");

            model.UserQuotesEditVM.Score = CheckAnswer(model.UserQuotesEditVM.SelectedAnswer, quote.Author.Name, model.authors[0].Name);

            bool result = model.UserQuotesEditVM.Score;

            model.msg = GenerateMsg(result, quote.Author.Name);
            model.quote.Text = quote.Text;
            KeepTrackOfPlayedGames(quote);

            if (AuthenticationManager.LoggedUser != null)
            {
                SaveUserQuote(model, quote);
            }

            return View("Play", model);

        }

        [HttpGet]
        public ActionResult SetMode()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SetMode(string GameMode)
        {
            if (GameMode == " Multiple choice questions " || GameMode == "Binary")
            {
                Session["GameMode"] = GameMode;
            }
            var com = Session["GameMode"].ToString();
            return RedirectToAction("Play");
        }


    }
}