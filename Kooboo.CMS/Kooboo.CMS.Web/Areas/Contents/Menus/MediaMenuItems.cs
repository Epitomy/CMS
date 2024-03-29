﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kooboo.Web.Mvc.Menu;
using System.Web.Mvc;

using Kooboo.Web.Mvc;
using Kooboo.CMS.Content.Models;
using Kooboo.CMS.Content.Services;
namespace Kooboo.CMS.Web.Areas.Contents.Menus
{
    public class MediaMenuItems : FolderMenuItems<MediaFolder>
    {
        protected override FolderManager<MediaFolder> FolderManager
        {
            get { return ServiceFactory.MediaFolderManager; }
        }
    }
}