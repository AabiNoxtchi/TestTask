using DataAccess.Entity;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using TestTask.Filters;
using TestTask.ViewModels.Shared;
using TestTask.ViewModels.Users;

namespace TestTask.Controllers
{
    [AuthorizeAdmin]
    public class UsersController : BaseController<User, UsersRepository, FilterVM, IndexVM, EditVM, OrderBy>
    {
       

    }
}