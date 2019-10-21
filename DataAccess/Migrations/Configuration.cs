namespace DataAccess.Migrations
{
    using DataAccess.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.Repository.TestTaskDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccess.Repository.TestTaskDbContext context)
        {

            if (!context.Users.Where(u=>u.IsAdmin).Any())
            {
                context.Users.Add(new Entity.User { UserName = "Admin",
                    Password = Convert.ToBase64String(ComputeHMAC_SHA256()),
                    IsAdmin = true });

            }

            if(!context.Authors.Any())
            {

                IList<Author> newAuthors = new List<Author>()
                {
                    new Author() { Name = "Oscar Wilde" ,Quotes=new List<Quote>()
                                                               {
                                                                     new Quote(){Text="Be yourself; everyone else is already taken." }
                                                               }
                                 } ,
                    new Author() { Name = "Albert Einstein" ,Quotes=new List<Quote>()
                                                               {
                                                                    new Quote(){Text="Two things are infinite: the universe and human stupidity; and I'm not sure about the universe." }
                                                               }
                                 },
                   new Author() {Name = "Dr. Seuss" ,Quotes=new List<Quote>()
                                                                {
                                                                     new Quote(){Text="Don't cry because it's over, smile because it happened." }
                                                                }
                                }
                };

                context.Authors.AddRange(newAuthors);
                context.SaveChanges();

            }


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }

        private byte[] ComputeHMAC_SHA256()
        {
            using (SHA384 shaM = new SHA384Managed())
            {
                byte[] adminPass = Encoding.UTF8.GetBytes("admin");
                return shaM.ComputeHash(adminPass);
            }
        }
    }
}
