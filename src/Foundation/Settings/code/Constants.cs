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
            public static readonly ID TrainingImageContainer = new ID("{393FD4F6-8835-432A-B804-CBA3EF0960C4}");
            public static readonly ID TrainingImageOutputContainer = new ID("{5938A47B-65FE-48E8-8329-C08D18A289D7}");
            public static readonly ID AzureBlobStorageConnectionString = new ID("{8A1A318C-06B6-470E-8717-A1E2F08A8234}");

            public static readonly ID PredictionEndpoint = new ID("{7939FD77-E18C-4567-AE91-8CA496A5847D}");
            public static readonly ID PredictionKey = new ID("{31A9A831-9008-48CC-80F2-580EDA2905A8}");
            public static readonly ID TrainingEndpoint = new ID("{2A840CA6-B92F-4223-86F0-D21DC3FA6BE1}");
            public static readonly ID TrainingKey = new ID("{119C5472-0A43-49E3-8EE1-791305958D02}");
            public static readonly ID PredictionResourceId = new ID("{94445052-5B4F-4A60-9806-F02EFC24DB54}");
            public static readonly ID ProjectId = new ID("{A37135DB-42A6-46E4-A4BB-BA00EE46D606}");
        }
    }
}