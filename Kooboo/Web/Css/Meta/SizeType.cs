﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kooboo.Web.Css.Meta
{
    public class SizeType : CombinedType
    {
        public static readonly EnumType AbsoluteSizeKeywords = new EnumType("medium | xx-small | x-small | small | large | x-large | xx-large");
        public static readonly EnumType RelativeSizeKeywords = new EnumType("larger | smaller");

        public SizeType()
            : base(AbsoluteSizeKeywords, RelativeSizeKeywords, PropertyValueType.Length)
        {
        }
    }
}
