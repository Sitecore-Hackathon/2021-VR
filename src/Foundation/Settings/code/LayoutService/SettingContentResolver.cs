using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.Mvc.Presentation;
using VRBYOD.Foundation.Settings.Service;
using static Sitecore.Configuration.Settings;

namespace VRBYOD.Foundation.Settings.LayoutService
{
    public class SettingContentResolver : RenderingContentsResolver
    {
        private readonly ISettingBuilder _settingBuilder;

        public SettingContentResolver(ISettingBuilder settingBuilder)
        {
            _settingBuilder = settingBuilder;
        }

        public override object ResolveContents(Sitecore.Mvc.Presentation.Rendering rendering, IRenderingConfiguration renderingConfig)
        {
            var contextItem = GetContextItem(rendering, renderingConfig);
            return new
            {
                Links = _settingBuilder.GetSettingItem(contextItem, rendering)
            };
        }
    }
}