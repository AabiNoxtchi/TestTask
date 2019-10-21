﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Author:BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Quote> Quotes { get; set; }
    }
}