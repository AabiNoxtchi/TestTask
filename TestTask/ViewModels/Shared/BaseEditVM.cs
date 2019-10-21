using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTask.ViewModels.Shared
{
  
        public abstract class BaseEditVM<E>
       where E : BaseEntity
        {
            public int Id { get; set; }

            public BaseEditVM() { }

            public abstract void PopulateModel(E item);
            public abstract void PopulateEntity(E item);
        }
   
}