﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kooboo.CMS.Sites.Extension.ModuleArea;
using Kooboo.ComponentModel;

namespace Kooboo.CMS.ModuleArea
{
    public class AdminControllerBase : ModuleAreaControllerBase
    {
        static AdminControllerBase()
        {
            TypeDescriptorHelper.RegisterMetadataType(typeof(ModuleSettings), typeof(Kooboo.CMS.ModuleArea.Models.ModuleSettings_Metadata));
        }
    }
}
