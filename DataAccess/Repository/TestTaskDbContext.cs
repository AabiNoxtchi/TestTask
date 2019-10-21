using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class TestTaskDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserQuote> UserQuotes { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Author> Authors { get; set; }


        public TestTaskDbContext() : base("TestTaskDbConnection")
        {
            this.Users = this.Set<User>();
            this.UserQuotes = this.Set<UserQuote>();
            this.Quotes = this.Set<Quote>();
            this.Authors = this.Set<Author>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           

        }


      
    }

}
