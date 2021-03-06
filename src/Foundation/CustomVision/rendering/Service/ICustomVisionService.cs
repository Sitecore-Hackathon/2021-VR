using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace VRBYOD.Foundation.CustomVision.Rendering.Service
{
    public interface ICustomVisionService
    {
        /// <summary>
        ///  Predict Image based on Training & Published Models
        /// </summary>
        /// <param name="imageData"></param>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public System.Drawing.Image PredictImage(System.Drawing.Image imageData, MemoryStream imageStream, string modelName);

        /// <summary>
        /// Train and Publish Model
        /// </summary>
        /// <param name="customModelName"></param>
        public void TrainProject(string customModelName);

        /// <summary>
        /// Clear Images from Project -- to start again
        /// </summary>
        public void PurgeImageFromProject();

        /// <summary>
        /// Get list of Trained Models
        /// </summary>
        /// <returns></returns>
        public List<string> GetPublishedModels();
    }
}
