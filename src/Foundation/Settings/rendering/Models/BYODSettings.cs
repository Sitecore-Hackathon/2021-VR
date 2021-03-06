using System;
using System.Collections.Generic;
using System.Text;

namespace VRBYOD.Foundation.Settings.Rendering.Models
{
    public class BYODSettings
    {
        public string TrainingImageContainer { get; set; }
        public string TrainingImageOutputContainer { get; set; }
        public string AzureBlobStorageConnectionString { get; set; }

        public string PredictionEndpoint { get; set; }
        public string PredictionKey { get; set; }
        public string TrainingEndpoint { get; set; }
        public string TrainingKey { get; set; }
        public string PredictionResourceId { get; set; }
        public string ProjectId { get; set; }
    }
}
