using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class QuotesRepository: BaseRepository<Quote>
    {
        public override void Save(Quote item)
        {
            if (item.Id <= 0) base.Save(item);
            else
                Update(item);
        }

        private void Update(Quote item)
        {
            foreach (UserQuote userQuote in item.UserQuotes)
            {
                Context.Entry(userQuote).State = EntityState.Modified;
               // Context.SaveChanges();
            }

            //item.UserQuotes = null;

            Context.Entry(item).State = EntityState.Modified;
            Context.SaveChanges();

           

            
        }

       
    }
}
