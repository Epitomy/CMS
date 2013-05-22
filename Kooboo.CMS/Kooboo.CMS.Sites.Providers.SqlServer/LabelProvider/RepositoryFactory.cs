﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kooboo.CMS.Sites.Globalization;

namespace Kooboo.CMS.Sites.Providers.SqlServer.LabelProvider
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public Kooboo.Globalization.IElementRepository CreateRepository(Models.Site site)
        {
            return new LabelRepository(site.FullName);
        }
    }
}
