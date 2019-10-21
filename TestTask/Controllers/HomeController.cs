using DataAccess.Entity;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TestTask.Services;
using TestTask.ViewModels.Home;

namespace TestTask.Controllers
{
    public class HomeController : Controller
    {
        
       

        [HttpGet]
        public ActionResult Register()
        {
            RegisterVM model = new RegisterVM();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UsersRepository repo = new UsersRepository();

            if (repo.GetAll(u => u.UserName == model.UserName).FirstOrDefault() != null)
            {
                ModelState.AddModelError("UserNameExist", "A Profile with this User name Already Exist");
                return View(model);
            }
            else
            {
                model.Password = Convert.ToBase64String(Hmac.ComputeHMAC_SHA256(Encoding.UTF8.GetBytes(model.Password)));
                User item = new User();
                model.PopulateEntity(item);
                repo.Save(item);
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            ModelState.Clear();

            return View(new LoginVM());
        }

        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            if (this.ModelState.IsValid)
            {
               
                model.Password = Convert.ToBase64String(Hmac.ComputeHMAC_SHA256(Encoding.UTF8.GetBytes(model.Password)));

                Models.AuthenticationManager.Authenticate(model.UserName, model.Password);

                if (Models.AuthenticationManager.LoggedUser == null)
                    ModelState.AddModelError("authenticationFailed", "Wrong username or password!");
            }

            if (!this.ModelState.IsValid)
            {
                return View(model);
            }


            return RedirectToAction("Play", "UserQuotes");
        }


        public ActionResult Logout()
        {
            Models.AuthenticationManager.Logout();
            Session["GameMode"] = null;
            Session["PlayedGames"] = null;
            return RedirectToAction("Login", "Home");
        }


    }
}