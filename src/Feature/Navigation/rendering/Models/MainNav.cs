using Sitecore.AspNet.RenderingEngine.Binding.Attributes;

namespace VRBYOD.Feature.Navigation.Rendering.Models
{
    public class MainNav
    {
        [SitecoreComponentField]
        public Link[] Links { get; set; }
    }
}