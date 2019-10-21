using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestTask.Filters
{
    public sealed class AllowAllAttribute:Attribute
    {
        public AllowAllAttribute() { }

    }
}