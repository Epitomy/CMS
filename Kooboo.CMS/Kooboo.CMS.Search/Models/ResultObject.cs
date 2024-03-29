﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kooboo.CMS.Search.Models
{
    public class ResultObject
    {
        public string Title { get; set; }
        public string HighlightedTitle { get; set; }
        public string Body { get; set; }
        public string HighlightedBody { get; set; }

        public string Url { get; set; }

        public object NativeObject { get; set; }
    }
}
