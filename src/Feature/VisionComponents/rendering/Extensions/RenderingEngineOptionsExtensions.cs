using VRBYOD.Feature.VisionComponents.Rendering.Models;
using Sitecore.AspNet.RenderingEngine.Configuration;
using Sitecore.AspNet.RenderingEngine.Extensions;

namespace VRBYOD.Feature.VisionComponents.Rendering.Extensions
{
    public static class RenderingEngineOptionsExtensions
    {
        public static RenderingEngineOptions AddFeatureVisionComponents(this RenderingEngineOptions options)
        {
            options.AddPartialView("Predict");
            options.AddPartialView("Train");
            return options;
        }
    }
}
