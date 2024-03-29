﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kooboo.CMS.Content.Models;

namespace Kooboo.CMS.Content.Persistence
{
    public interface ITextContentFileProvider
    {
        string Save(TextContent content, ContentFile file);
        void DeleteFiles(TextContent content);
    }
}
