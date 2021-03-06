using VRBYOD.Foundation.ImageProcessing.Service;
using Sitecore.Data;
using Sitecore.Web.UI.Sheer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VRBYOD.Foundation.ImageProcessing.Pipelines.Processor
{
    public class DeleteMediaProcessor
    {
        public List<string> Config { get; private set; }
        private AzureStorage storage;
        public DeleteMediaProcessor()
        {
            Config = new List<string>();
            storage = new AzureStorage();
        }

        public void Delete(ClientPipelineArgs args)
        {
            try
            {
                var items = args.Parameters["items"];
                foreach (var itemID in items?.Split("|".ToCharArray()))
                {
                    var db = Sitecore.Configuration.Factory.GetDatabase(args.Parameters["database"]);
                    var item = db.GetItem(new ID(itemID));
                    if (EnsureFolderPath(item.Paths.FullPath))
                    {
                        var mediaItem = new Sitecore.Data.Items.MediaItem(item);
                        storage.Delete($"{mediaItem.Name}.{mediaItem.Extension}");
                    }
                }
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.SingleError("Error while deleting media item - Azure Blob is not removed" + ex, ex);
            }
        }

        private bool EnsureFolderPath(string folder)
        {
            return Config.Any(location => folder.ToLower().StartsWith(location.ToLower()));
        }
    }
}