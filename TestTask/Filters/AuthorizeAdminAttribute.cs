using System.Web.Mvc;
using TestTask.Models;

namespace TestTask.Filters
{
    public class AuthorizeAdminAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool IsAuthenticAttribute =
        filterContext.ActionDescriptor.IsDefined(typeof(AllowAllAttribute), true);

            if (!IsAuthenticAttribute)
            {
                if (AuthenticationManager.LoggedUser == null || AuthenticationManager.LoggedUser != null && !AuthenticationManager.LoggedUser.IsAdmin)
                    filterContext.Result = new RedirectResult("/Home/Login");
            }


           
        }
    }
}