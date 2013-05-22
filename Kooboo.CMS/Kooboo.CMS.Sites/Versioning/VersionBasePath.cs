﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kooboo.CMS.Sites.Models;
using System.IO;

namespace Kooboo.CMS.Sites.Versioning
{
    public class VersionBasePath
    {
        private static string VersionPathName = "Versions";
        public VersionBasePath(DirectoryResource dir)
        {
            var basePath = Path.Combine(Kooboo.Settings.BaseDirectory, PathEx.BasePath);
            var versionPath = Path.Combine(basePath, VersionPathName);
            this.PhysicalPath = dir.PhysicalPath.Replace(basePath, versionPath);            
        }
        public string PhysicalPath { get; set; }
        public bool Exists()
        {
            return Directory.Exists(this.PhysicalPath);
        }
    }
    public class VersionPath
    {
        public VersionPath(DirectoryResource dir, int version)
        {
            var versionBasePath = new VersionBasePath(dir);
            this.PhysicalPath = Path.Combine(versionBasePath.PhysicalPath, version.ToString());
        }
        public string PhysicalPath { get; set; }
        public bool Exists()
        {
            return Directory.Exists(this.PhysicalPath);
        }
    }
}
