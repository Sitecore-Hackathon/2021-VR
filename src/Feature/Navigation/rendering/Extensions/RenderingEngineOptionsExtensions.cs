using VRBYOD.Feature.Navigation.Rendering.Models;
using Sitecore.AspNet.RenderingEngine.Configuration;
using Sitecore.AspNet.RenderingEngine.Extensions;

namespace VRBYOD.Feature.Navigation.Rendering.Extensions
{
    public static class RenderingEngineOptionsExtensions
    {
        public static RenderingEngineOptions AddFeatureNavigation(this RenderingEngineOptions options)
        {
            options.AddModelBoundView<TopLinks>("TopLinks")
                   .AddModelBoundView<MainNav>("MainNav")
                   .AddModelBoundView<Footer>("Footer");
            return options;
        }
    }
}
