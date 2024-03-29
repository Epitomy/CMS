﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kooboo.CMS.Sites.Parsers.ThemeRule
{
    public interface IThemeRuleParser
    {
        IEnumerable<string> Parse(string cssHackBody, Func<string, string> replaceUrl, out string replacedCssHackBody);
    }
}
