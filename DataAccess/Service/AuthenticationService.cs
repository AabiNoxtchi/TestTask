using DataAccess.Entity;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class AuthenticationService
    {
        public User LoggedUser { get; private set; }

        public void AuthenticateUser(string userName, string password)
        {
            UsersRepository userRepo = new UsersRepository();
            LoggedUser = userRepo.GetAll(u => u.UserName == userName && u.Password == password ).FirstOrDefault();
        }
    }
}
