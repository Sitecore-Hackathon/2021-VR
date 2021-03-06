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

        private string _containerName
        {
            get { return Settings.GetSetting("AzureBlob.Container"); }
        }
        private string _connectionString
        {
            get { return Settings.GetSetting("AzureBlob.ConnectionString"); }
        }

        public AzureStorage()
        {
            _blobContainer = new BlobContainerClient(_connectionString, _containerName);
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