using System.Text.RegularExpressions;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Resources.Media;
using Sitecore.StringExtensions;
using Azure.Storage.Blobs;

namespace VRBYOD.Foundation.ImageProcessing.Service
{
    public class AzureStorage
    {
        private BlobContainerClient _blobContainer;

        private Item _settingItem
        {
            get
            {
                var db = Sitecore.Context.ContentDatabase ?? Sitecore.Context.Database;
                return db.GetItem(new Sitecore.Data.ID(Sitecore.Configuration.Settings.GetSetting("VRBYOD-Setting-Item")
              ));

            }
        }

        public AzureStorage()
        {
            //Init Container client
            _blobContainer = new BlobContainerClient(_settingItem.Fields[Constants.Settings.AzureBlobStorageConnectionString].Value, _settingItem.Fields[Constants.Settings.TrainingImageContainer].Value);
        }

        public string UploadMedia(MediaItem media)
        {
            //Create FileName with Extenstion
            string filename = $"{media.InnerItem.Name}.{media.Extension}";
            BlobClient blob = _blobContainer.GetBlobClient(filename);
            using (var fileStream = media.GetMediaStream())
            {
                blob.Upload(fileStream);
            }
            Log.Info($"Uploaded Training Image --> {filename}  To Azure Blob Storage", this);
            return filename;
        }

        public string Update(MediaItem media)
        {
            return UploadMedia(media);
        }

        public bool Delete(string filename)
        {
            BlobClient blob = _blobContainer.GetBlobClient(filename);
            return blob.DeleteIfExists();
        }
    }
}