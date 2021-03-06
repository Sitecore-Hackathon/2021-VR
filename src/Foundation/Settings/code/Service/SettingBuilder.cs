using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VRBYOD.Foundation.Settings.Model;

namespace VRBYOD.Foundation.Settings.Service
{
    public class SettingBuilder : ISettingBuilder
    {

        private string _byodSettingItem
        {
            get { return Sitecore.Configuration.Settings.GetSetting("VRBYOD-Setting-Item"); }
        }
        /// <summary>
        /// Returns BYOD Setting item to rendering host :: NOTE THIS IS NOT a proper solution - 
        /// NEVER PASS DATA through resolvers - Store it in setting.json file in rendering host
        /// </summary>
        /// <param name="contextItem"></param>
        /// <param name="rendering"></param>
        /// <returns></returns>

        public BYODSettings GetSettingItem(Sitecore.Data.Items.Item contextItem, Sitecore.Mvc.Presentation.Rendering rendering)
        {
            var SettingItem = contextItem.Database.GetItem(new Sitecore.Data.ID(_byodSettingItem));
            return new BYODSettings()
            {
                // AzureBlobStorageConnectionString = SettingItem.Fields[Constants.SettingsTemplate.AzureBlobStorageConnectionString].Value,
                PredictionEndpoint = SettingItem.Fields[Constants.SettingsTemplate.PredictionEndpoint].Value,
                PredictionKey = SettingItem.Fields[Constants.SettingsTemplate.PredictionKey].Value,
                PredictionResourceId = SettingItem.Fields[Constants.SettingsTemplate.PredictionResourceId].Value,
                ProjectId = SettingItem.Fields[Constants.SettingsTemplate.ProjectId].Value,
                TrainingEndpoint = SettingItem.Fields[Constants.SettingsTemplate.TrainingEndpoint].Value,
                TrainingImageContainer = SettingItem.Fields[Constants.SettingsTemplate.TrainingImageContainer].Value,
                TrainingImageOutputContainer = SettingItem.Fields[Constants.SettingsTemplate.TrainingImageOutputContainer].Value,
                TrainingKey = SettingItem.Fields[Constants.SettingsTemplate.TrainingKey].Value
            };
        }
    }
}