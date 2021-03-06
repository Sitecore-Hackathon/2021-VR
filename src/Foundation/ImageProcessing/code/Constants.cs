using Sitecore.Data;

namespace VRBYOD.Foundation.ImageProcessing
{
    public class Constants
    {
        /// <summary>
        /// Fields used in custom media library Item
        /// </summary>
        public static string HashFieldName = "MediaHash";
        public static string MediaFilePath = "FilePath";
        public static string SyncedToCloud = "SyncToCloud";

        public static ID TrainingImageTemplate = new ID("{8F988373-4A8E-48FB-8BE7-BFD3B34082E7}");

        public static class Settings
        {

            public static readonly ID TrainingImageContainer = new ID("{DA4B4B8A-D927-471A-81F6-BBA3F6C5301E}");
            public static readonly ID TrainingImageOutputContainer = new ID("{9C9CD775-A766-4824-9423-497761C61062}");
            public static readonly ID AzureBlobStorageConnectionString = new ID("{78090A6F-875F-4371-AD64-8F52C319BC20}");
        }
    }
}