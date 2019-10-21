using System.Web.Mvc;
using TestTask.Models;

namespace TestTask.Filters
{
    public class AuthenticationFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AuthenticationManager.LoggedUser == null)
                filterContext.Result = new RedirectResult("/Home/Login");
        }
    }
}