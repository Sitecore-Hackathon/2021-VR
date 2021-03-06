using VRBYOD.Feature.SimpleText.Rendering.Models;
using Sitecore.AspNet.RenderingEngine.Configuration;
using Sitecore.AspNet.RenderingEngine.Extensions;

namespace VRBYOD.Feature.SimpleText.Rendering.Extensions
{
    public static class RenderingEngineOptionsExtensions
    {
        public static RenderingEngineOptions AddFeatureSimpleText(this RenderingEngineOptions options)
        {
            options.AddModelBoundView<RichTextContent>("RichTextContent");

            return options;
        }
    }
}
