using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTask.ViewModels.Shared
{

    public class PagerVM
    {
        public string Prefix { get; set; }

        public int Page { get; set; }
        public int PagesCount { get; set; }
        public int ItemsPerPage { get; set; }
    }
}