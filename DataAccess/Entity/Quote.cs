using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Quote:BaseEntity
    {
      
        public string Text { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public ICollection<UserQuote> UserQuotes { get; set; }

        
    }
}
