using VRBYOD.Foundation.ImageProcessing.Service;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.Upload;
using Sitecore.SecurityModel;
using System.Linq;

namespace VRBYOD.Foundation.ImageProcessing.Pipelines.Processor
{
    public class UploadToAzure
    {
        private AzureStorage storage;
        public UploadToAzure()
        {
            storage = new AzureStorage();
        }

        public void Process(UploadArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            foreach (Item file in args.UploadedItems.Where(file => file.Paths.IsMediaItem))
            {
                // upload to CDN
                string filename = storage.Update(file);

                // update the item file location to CDN
                using (new EditContext(file, SecurityCheck.Disable))
                {
                    file[Constants.MediaFilePath] = filename;
                    file[Constants.SyncedToCloud] = "1";
                }
            }
        }
    }
}