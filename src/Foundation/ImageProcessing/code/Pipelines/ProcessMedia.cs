using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines;
using Sitecore.Pipelines.Upload;

namespace VRBYOD.Foundation.ImageProcessing.Pipelines
{
    public class ProcessMedia : UploadProcessor
    {
        public void Process(UploadArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            ///Check if Training Image
            if (args.CustomData["IsTrainingFile"] != null)
            {
                ///Switching template to Training Template
                var db = Sitecore.Context.ContentDatabase;
                var trainingTemplateItem = db.GetItem(Constants.TrainingImageTemplate);
                ///Switch to custom Media Item
                foreach (Item mediaItem in args.UploadedItems)
                {
                    mediaItem.Editing.BeginEdit();
                    mediaItem.ChangeTemplate(new TemplateItem(trainingTemplateItem));
                    mediaItem.Editing.EndEdit();
                }
                StartJob(args);
            }
        }

        /// <summary>
        /// Creates and starts a Sitecore Job to run as a long running background task
        /// </summary>
        /// <param name="args">The UploadArgs</param>
        public void StartJob(UploadArgs args)
        {
            ////Run Azure Media Processor Job
            var jobOptions = new Sitecore.Jobs.DefaultJobOptions("VRBYOD.Azure.MediaProcessor", "MediaProcessing",
                                                          Sitecore.Context.Site.Name,
                                                          this, "Run", new object[] { args });
            Sitecore.Jobs.JobManager.Start(jobOptions);
        }

        /// <summary>
        /// Calls Custom Pipeline with the supplied args
        /// </summary>
        /// <param name="args">The UploadArgs</param>
        public void Run(UploadArgs args)
        {
            CorePipeline.Run("VRBYOD.Azure.MediaProcessor", args);
        }
    }
}