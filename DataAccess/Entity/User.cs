using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class User:BaseEntity
    {

        public string UserName { get; set; }

        public string Password { get; set; }
       
        public bool IsAdmin { get; set; }

        public ICollection<UserQuote> UserQuotes { get; set; }
    }
}
