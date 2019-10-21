using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class UserQuote:BaseEntity , ISoftDeletable
    {
        public bool Score { get; set; }

        public int UserId { get; set; }

        public int QuoteId { get; set; }

        public string SelectedAnswer { get; set; }

        public string CorrectAuthorName { get; set; }

        public string ShownAnswer { get; set; }

        //[Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public DateTime CreatedAt { get; set; }

        public User User { get; set; }

        public Quote Quote { get; set; }

        public bool IsDeleted { get ; set ; }
    }
}
