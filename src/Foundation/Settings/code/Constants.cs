using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VRBYOD.Foundation.Settings
{
    public static class Constants
    {
        public static class SettingsTemplate
        {
            public static readonly ID TrainingImageContainer = new ID("{DA4B4B8A-D927-471A-81F6-BBA3F6C5301E}");
            public static readonly ID TrainingImageOutputContainer = new ID("{9C9CD775-A766-4824-9423-497761C61062}");
            public static readonly ID AzureBlobStorageConnectionString = new ID("{78090A6F-875F-4371-AD64-8F52C319BC20}");

            public static readonly ID PredictionEndpoint = new ID("{A2BD0E25-1187-4B19-9DDA-D67C7A88590E}");
            public static readonly ID PredictionKey = new ID("{56CD18A5-86D6-41E6-96F4-F8C5D27C4CE7}");
            public static readonly ID TrainingEndpoint = new ID("{4279F4A6-D9D6-403D-9EDB-D20D14A7BD87}");
            public static readonly ID TrainingKey = new ID("{115EFDC5-AC77-456F-B510-BB09A5D6F923}");
            public static readonly ID PredictionResourceId = new ID("{D1DC7512-4617-4014-9E65-1EF8522BD5BA}");
            public static readonly ID ProjectId = new ID("{0817D52A-64B1-43D5-9AC8-CE50708CD0DA}");
        }
    }
}