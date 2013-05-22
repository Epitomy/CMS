﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kooboo.CMS.Caching
{
    public static class CacheExpiredNotification
    {
        public static List<INotifyCacheExpired> Notifiactions = new List<INotifyCacheExpired>();
        public static void Notify(string objectCacheName, string cacheKey)
        {
            if (Notifiactions != null)
            {
                try
                {
                    foreach (var item in Notifiactions)
                    {
                        item.Notify(objectCacheName, cacheKey);
                    }
                }
                catch (Exception e)
                {
                    Kooboo.HealthMonitoring.Log.LogException(e);
                }

            }
        }
    }
}
