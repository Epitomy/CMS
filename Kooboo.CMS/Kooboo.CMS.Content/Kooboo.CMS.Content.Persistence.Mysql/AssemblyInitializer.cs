﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
[assembly: System.Web.PreApplicationStartMethod(typeof(Kooboo.CMS.Content.Persistence.Mysql.AssemblyInitializer), "Initialize")]
namespace Kooboo.CMS.Content.Persistence.Mysql
{
    public static class AssemblyInitializer
    {
        public static void Initialize()
        {
            ApplicationInitialization.RegisterInitializerMethod(delegate()
            {
                Kooboo.CMS.Content.Persistence.Providers.DefaultProviderFactory = new Mysql.ProviderFactory();
            }, 0);
        }
    }
}
