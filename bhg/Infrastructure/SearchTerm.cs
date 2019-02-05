﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Infrastructure
{
    public class SearchTerm
    {
        public string Name { get; set; }

        public string Operator { get; set; }

        public string Value { get; set; }

        public bool ValidSyntax { get; set; }

        public ISearchExpressionProvider ExpressionProvider { get; set; }
    }
}
