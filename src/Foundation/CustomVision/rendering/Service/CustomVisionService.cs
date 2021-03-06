using VRBYOD.Foundation.CustomVision.Rendering.Helpers;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace VRBYOD.Foundation.CustomVision.Rendering.Service
{
    public class CustomVisionService : ICustomVisionService
    {
        private readonly IConfiguration _configuration;
        private string _predictionEndpoint
        {
            get { return _configuration.GetValue<string>("Foundation:Trainer:predictionEndpoint"); }
        }
        private string _trainerEndpoint
        {
            get { return _configuration.GetValue<string>("Foundation:Trainer:trainingEndpoint"); }
        }
        private string _predictionKey
        {
            get { return _configuration.GetValue<string>("Foundation:Trainer:predictionKey"); }
        }
        private string _trainerKey
        {
            get { return _configuration.GetValue<string>("Foundation:Trainer:trainingKey"); }
        }
        private string _projectId
        {
            get { return _configuration.GetValue<string>("Foundation:Trainer:projectId"); }
        }
        private string _predictionResourceId
        {
            get { return _configuration.GetValue<string>("Foundation:Trainer:predictionResourceId"); }
        }

        public CustomVisionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// Create Training Client
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="trainingKey"></param>
        /// <returns></returns>
        private CustomVisionTrainingClient AuthenticateTraining(string endpoint, string trainingKey)
        {
            // Create the Api, passing in the training key
            CustomVisionTrainingClient trainingApi = new CustomVisionTrainingClient(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.ApiKeyServiceClientCredentials(trainingKey))
            {
                Endpoint = endpoint
            };
            return trainingApi;
        }
        /// <summary>
        /// Create Prediction Client
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="predictionKey"></param>
        /// <returns></returns>
        private CustomVisionPredictionClient AuthenticatePrediction(string endpoint, string predictionKey)
        {
            // Create a prediction endpoint, passing in the obtained prediction key
            CustomVisionPredictionClient predictionApi = new CustomVisionPredictionClient(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.ApiKeyServiceClientCredentials(predictionKey))
            {
                Endpoint = endpoint
            };
            return predictionApi;
        }

        /// <summary>
        /// Predicts Image
        /// </summary>
        /// <param name="imageData"></param>
        /// <returns></returns>
        public System.Drawing.Image PredictImage(System.Drawing.Image imageData, MemoryStream imageStream, string modelName)
        {
            //Create Prediction Api
            var predictionApi = AuthenticatePrediction(_predictionEndpoint, _predictionKey);

            ///TODO: It is messy Code to write between FILE Streams !! But this is somehow we could overcome DOCKER Watcher
            using (FileStream file = new FileStream(GetTempFileLocation(), FileMode.Open, FileAccess.ReadWrite))
            {
                imageStream.WriteTo(file);
            }

            using (FileStream file = new FileStream(GetTempFileLocation(), FileMode.Open, FileAccess.Read))
            {
                //Detect image bounding boxes using PredictioApi
                var result = predictionApi.DetectImage(new Guid(_projectId), modelName, file);
                return ImageHelper.DrawBoundingBox(imageData, result.Predictions.ToList());
            }


        }

        /// <summary>
        /// Get Published model name
        /// </summary>
        /// <returns></returns>
        public List<string> GetPublishedModels()
        {
            ///Create Training Api
            var trainingApi = AuthenticateTraining(_trainerEndpoint, _trainerKey);
            //Get Project
            var project = trainingApi.GetProject(new Guid(_projectId));
            var iterations = trainingApi.GetIterations(project.Id);
            //Get Published Name
            return iterations?.Where(x => !string.IsNullOrEmpty(x.PublishName) && (x.Status.ToLower() == "published" || x.Status.ToLower() == "completed"))?.Select(x => x.PublishName).ToList();
        }

        /// <summary>
        /// Train your custom project in Custom Vistion AI
        /// </summary>
        public string TrainProject(string customModelName)
        {
            ///Create Training Api
            var trainingApi = AuthenticateTraining(_trainerEndpoint, _trainerKey);
            //Get Project
            var project = trainingApi.GetProject(new Guid(_projectId));
            try
            {
                ///Train project - Returns iteration
                var iteration = trainingApi.TrainProject(project.Id);

                // The returned iteration will be in progress, and can be queried periodically to see when it has completed
                while (iteration.Status == "Training")
                {
                    // Re-query the iteration to get it's updated status
                    iteration = trainingApi.GetIteration(project.Id, iteration.Id);
                    /// TODO : Use Hub to post iteration Info
                }

                //Now it's time to publish iteration
                PublishIteration(trainingApi, project, iteration, customModelName);
                return "Model Trained and Published Successfully";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// Purge Images from Project - to help clear data and start over again using voTT app
        /// </summary>
        public void PurgeImageFromProject()
        {
            ///Create Training Api
            var trainingApi = AuthenticateTraining(_trainerEndpoint, _trainerKey);
            // Delete all images within project
            trainingApi.DeleteImages(new Guid(_projectId), allImages: true);
        }

        #region helpers
        private void PublishIteration(CustomVisionTrainingClient trainingApi, Project project, Iteration iteration, string modelName)
        {
            trainingApi.PublishIteration(project.Id, iteration.Id, modelName, _predictionResourceId);
            
            // Now there is a trained endpoint, it can be used to make a prediction
        }

        private string GetTempFileLocation()
        {
            //App root
            var appRoot = _configuration.GetValue<string>(Microsoft.AspNetCore.Hosting.WebHostDefaults.ContentRootKey);
            var fileName = $"TestImage.jpg";
            var directoryPath = Path.Combine(appRoot, "wwwroot", "Object-Detection-Images");
            bool exists = System.IO.Directory.Exists(directoryPath);
            if (!exists)
                System.IO.Directory.CreateDirectory(directoryPath);

            var imageFile = Path.Combine(directoryPath, fileName);
            return imageFile;
        }

        public string CheckIfIterationInTraining()
        {
            ///Create Training Api
            var trainingApi = AuthenticateTraining(_trainerEndpoint, _trainerKey);
            //Get Project
            var project = trainingApi.GetProject(new Guid(_projectId));
            var iterations = trainingApi.GetIterations(project.Id);
            //Get Published Name
            return iterations?.Where(x => x.Status.ToLower() == "training")?.Select(x => x.Name).FirstOrDefault();
        }

        #endregion
    }
}
