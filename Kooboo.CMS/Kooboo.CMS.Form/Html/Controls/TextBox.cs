﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Kooboo.CMS.Form.Html.Controls
{
    public class TextBox : Input
    {
        public override string Name
        {
            get { return "TextBox"; }
        }
        public override string Type
        {
            get { return "text"; }
        }
    }
}
