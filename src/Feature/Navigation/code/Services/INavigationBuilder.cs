using VRBYOD.Feature.Navigation.Models;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System.Collections.Generic;

namespace VRBYOD.Feature.Navigation.Services
{
    public interface INavigationBuilder
    {
        IList<Link> GetNavigationLinks(Item contextItem, Rendering rendering);
    }
}