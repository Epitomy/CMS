﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using Kooboo.CMS.Content.Models.Paths;
using Kooboo.CMS.Content.Models;
using System.Runtime.Serialization;
using Kooboo.Runtime.Serialization;
using Ionic.Zip;
using Kooboo.CMS.Content.Services;
using System.IO;
namespace Kooboo.CMS.Content.Persistence.AzureBlobService
{

    public class MediaFolders
    {
        static System.Threading.ReaderWriterLockSlim locker = new System.Threading.ReaderWriterLockSlim();
        public static void AddFolder(MediaFolder folder)
        {
            locker.EnterWriteLock();
            try
            {
                var folders = GetList(folder.Repository);
                if (!folders.ContainsKey(folder.FullName))
                {
                    folders[folder.FullName] = folder;
                    SaveList(folder.Repository, folders);
                }
            }
            finally
            {
                locker.ExitWriteLock();
            }
        }
        public static MediaFolder GetFolder(MediaFolder dummy)
        {
            locker.EnterReadLock();
            try
            {
                var folders = GetList(dummy.Repository);
                if (folders.ContainsKey(dummy.FullName))
                {
                    return ToMediaFolder(dummy.Repository, dummy.FullName, folders[dummy.FullName]);
                }
                return null;
            }
            finally
            {
                locker.ExitReadLock();
            }

        }
        public static void RemoveFolder(MediaFolder folder)
        {
            locker.EnterWriteLock();
            try
            {
                var storeList = GetList(folder.Repository);
                var mediaFolders = ToMediaFolders(folder.Repository, storeList);
                if (storeList.ContainsKey(folder.FullName))
                {
                    storeList.Remove(folder.FullName);

                    foreach (var item in mediaFolders)
                    {
                        if (item.Parent == folder)
                        {
                            if (storeList.ContainsKey(item.FullName))
                            {
                                storeList.Remove(item.FullName);
                            }
                        }
                    }
                }
                SaveList(folder.Repository, storeList);
            }
            finally
            {
                locker.ExitWriteLock();
            }

        }
        public static void UpdateFolder(MediaFolder folder)
        {
            locker.EnterWriteLock();
            try
            {
                var folders = GetList(folder.Repository);
                folders[folder.FullName] = folder;
                SaveList(folder.Repository, folders);
            }
            finally
            {
                locker.ExitWriteLock();
            }

        }

        public static IEnumerable<MediaFolder> RootFolders(Repository repository)
        {
            locker.EnterReadLock();
            try
            {
                return ToMediaFolders(repository, GetList(repository)).Where(it => it.Parent == null);
            }
            finally
            {
                locker.ExitReadLock();
            }

        }
        public static IEnumerable<MediaFolder> ChildFolders(MediaFolder parent)
        {
            locker.EnterReadLock();
            try
            {
                return ToMediaFolders(parent.Repository, GetList(parent.Repository)).Where(it => it.Parent == parent);
            }
            finally
            {
                locker.ExitReadLock();
            }

        }
        private static IEnumerable<MediaFolder> ToMediaFolders(Repository repository, Dictionary<string, MediaFolder> folders)
        {
            return folders.Select(it => ToMediaFolder(repository, it.Key, it.Value)).ToArray();
        }
        private static MediaFolder ToMediaFolder(Repository repository, string fullName, MediaFolder folderProperties)
        {
            return new MediaFolder(repository, fullName)
            {
                DisplayName = folderProperties.DisplayName,
                UserId = folderProperties.UserId,
                UtcCreationDate = folderProperties.UtcCreationDate,
                AllowedExtensions = folderProperties.AllowedExtensions
            };
        }
        public static Dictionary<string, MediaFolder> GetList(Repository repository)
        {
            var container = MediaBlobHelper.InitializeRepositoryContainer(repository);

            var blobClient = CloudStorageAccountHelper.GetStorageAccount().CreateCloudBlobClient();

            var folderBlob = container.GetBlobReference(MediaBlobHelper.MediaDirectoryName);

            Dictionary<string, MediaFolder> folders = null;
            try
            {

                folderBlob.FetchAttributes();
                if (folderBlob.CheckIfMediaFolder())
                {
                    var xml = folderBlob.DownloadText();
                    folders = DataContractSerializationHelper.DeserializeFromXml<Dictionary<string, MediaFolder>>(xml);
                }

            }
            catch { }

            if (folders == null)
            {
                folders = new Dictionary<string, MediaFolder>();
            }
            return new Dictionary<string, MediaFolder>(folders, StringComparer.OrdinalIgnoreCase);
        }
        private static void SaveList(Repository repository, Dictionary<string, MediaFolder> folders)
        {
            if (folders != null && folders.Count > 0)
            {
                var xml = DataContractSerializationHelper.SerializeAsXml(folders);

                var container = MediaBlobHelper.InitializeRepositoryContainer(repository);

                var blobClient = CloudStorageAccountHelper.GetStorageAccount().CreateCloudBlobClient();

                var folderBlob = container.GetBlobReference(MediaBlobHelper.MediaDirectoryName);

                folderBlob.SetMediaFolderContentType();

                BlobRequestOptions header = new BlobRequestOptions();

                folderBlob.UploadText(xml);
            }
            else
            {
                var container = MediaBlobHelper.InitializeRepositoryContainer(repository);

                var blobClient = CloudStorageAccountHelper.GetStorageAccount().CreateCloudBlobClient();

                var folderBlob = container.GetBlobReference(MediaBlobHelper.MediaDirectoryName);

                folderBlob.DeleteIfExists();
            }
        }

    }
    public class MediaFolderProvider : IMediaFolderProvider
    {
        public IQueryable<MediaFolder> ChildFolders(MediaFolder parent)
        {
            return MediaFolders.ChildFolders(parent).AsQueryable();
        }

        public IQueryable<MediaFolder> All(Repository repository)
        {
            return MediaFolders.RootFolders(repository).AsQueryable();
        }

        //public IEnumerable<MediaFolder> EnumerateFolders(Repository repository, string parentBlobPath)
        //{
        //    var container = MediaBlobHelper.InitializeRepositoryContainer(repository);

        //    var blobClient = CloudStorageAccountHelper.GetStorageAccount().CreateCloudBlobClient();

        //    var subDirList = blobClient.GetBlobDirectoryReference(parentBlobPath).ListBlobs(new BlobRequestOptions() { UseFlatBlobListing = false });
        //}

        public MediaFolder Get(MediaFolder dummy)
        {
            return MediaFolders.GetFolder(dummy);
        }

        public void Add(MediaFolder item)
        {
            MediaFolders.AddFolder(item);
        }

        //private static void SubmitMediaFolder(MediaFolder item)
        //{
        //    var container = MediaBlobHelper.InitializeRepositoryContainer(item.Repository);

        //    var blobClient = CloudStorageAccountHelper.GetStorageAccount().CreateCloudBlobClient();

        //    var folderBlob = container.GetBlobReference(item.GetMediaBlobPath());


        //    folderBlob.Metadata["FullName"] = item.FullName;
        //    folderBlob.Metadata["DisplayName"] = item.DisplayName;
        //    folderBlob.Metadata["UserId"] = item.UserId;

        //    folderBlob.SetMediaFolderContentType();

        //    BlobRequestOptions header = new BlobRequestOptions();

        //    folderBlob.UploadByteArray(new byte[0]);
        //}


        public void Update(MediaFolder @new, MediaFolder old)
        {
            MediaFolders.UpdateFolder(@new);
        }

        public void Remove(MediaFolder item)
        {
            MediaFolders.RemoveFolder(item);
            (new MediaContentProvider()).Delete(item);
        }


        public void Export(Repository repository, IEnumerable<MediaFolder> models, System.IO.Stream outputStream)
        {
            throw new NotImplementedException();
        }

        public void Import(Repository repository, MediaFolder folder, System.IO.Stream zipStream, bool @override)
        {
            using (ZipFile zipFile = ZipFile.Read(zipStream))
            {
                foreach (ZipEntry item in zipFile)
                {
                    if (item.IsDirectory)
                    {

                    }
                    else
                    {
                        var path = Path.GetDirectoryName(item.FileName);
                        var fileName = Path.GetFileName(item.FileName);
                        var currentFolder = CreateMediaFolderByPath(folder, path);
                        Add(currentFolder);
                        var stream = new MemoryStream();
                        item.Extract(stream);
                        stream.Position = 0;
                        ServiceFactory.MediaContentManager.Add(repository, currentFolder,
                            fileName, stream, true);
                    }
                }
            }
        }
        private MediaFolder CreateMediaFolderByPath(MediaFolder folder, string pathName)
        {
            return new MediaFolder(folder.Repository, pathName, folder);
        }
    }
}
