﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Kooboo.CMS.Sites.Extension.ModuleArea
{
    public class ModuleResultExecutedContext : ResultExecutedContext
    {
        public ModuleResultExecutedContext(ControllerContext controllerContext, ActionResult result, bool canceled, Exception exception, string resultHtml) :
            base(controllerContext, result, canceled, exception)
        {
            this.ResultHtml = resultHtml;
        }
        public string ResultHtml { get; private set; }
    }
}
