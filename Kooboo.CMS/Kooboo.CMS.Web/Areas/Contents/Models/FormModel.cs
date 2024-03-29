﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kooboo.CMS.Content.Models;
using System.Web.Mvc;

namespace Kooboo.CMS.Web.Areas.Contents.Models
{
    public class FormModel
    {
        public FormType FormType { get; set; }
        [AllowHtml]
        public string Body { get; set; }
    }
}