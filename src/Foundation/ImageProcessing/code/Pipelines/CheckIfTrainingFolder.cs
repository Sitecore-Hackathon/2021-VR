﻿using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.Upload;
using System.Collections.Generic;
using System.Linq;

namespace VRBYOD.Foundation.ImageProcessing.Pipelines
{
    public class CheckIfTrainingFolder : UploadProcessor
    {
        public List<string> Config { get; private set; }

        public CheckIfTrainingFolder()
        {
            Config = new List<string>();
        }

        public void Process(UploadArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            if (args.Destination == UploadDestination.Database && EnsureUploadAsFile(args.Folder))
            {
                //args.Destination = UploadDestination.File;
                args.CustomData["IsTrainingFile"] = true;
            }
        }

        /// <summary>
        /// Checks if passed in folder is configured to force upload as file
        /// </summary>
        /// <param name="folder">Location current item is being uploaded to</param>
        /// <returns>boolean</returns>
        private bool EnsureUploadAsFile(string folder)
        {
            Database db = Sitecore.Context.ContentDatabase ?? Sitecore.Context.Database;
            folder = db.GetItem(folder).Paths.FullPath.ToLower();

            return Config.Any(location => folder.StartsWith(location.ToLower()));
        }
    }
}