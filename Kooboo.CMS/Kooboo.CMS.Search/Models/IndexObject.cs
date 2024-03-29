﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Kooboo.CMS.Search.Models
{
    public class IndexObject
    {
        public string Title { get; set; }
        public string Body { get; set; }

        public string NativeType { get; set; }
        public NameValueCollection StoreFields { get; set; }
        public NameValueCollection SystemFields { get; set; }
    }
}
