﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kooboo.Web.Mvc.Menu;
using Kooboo.Web.Mvc;
using Kooboo.CMS.Content.Models;
using Kooboo.CMS.Web.Authorizations;
namespace Kooboo.CMS.Web.Areas.Contents.Menus
{
    public class WorkflowRootMenuInitializer : AuthorizeMenuItemInitializer
    {
        protected override bool GetIsVisible(MenuItem menuItem, System.Web.Mvc.ControllerContext controllerContext)
        {
            var visible = false;
            if (Repository.Current != null)
            {
                visible = Repository.Current.EnableWorkflow;
            }
            return visible;
        }
    }
    public class WorkflowMenuInitializer : AuthorizeMenuItemInitializer
    {
        protected override bool GetIsVisible(MenuItem menuItem, System.Web.Mvc.ControllerContext controllerContext)
        {
            var visible = false;
            if (Repository.Current != null)
            {
                visible = Repository.Current.EnableWorkflow;
            }
            if (visible)
            {
                visible = base.GetIsVisible(menuItem, controllerContext);
            }
            return visible;
        }
    }
}