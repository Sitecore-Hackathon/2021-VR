using Microsoft.Extensions.DependencyInjection;
using VRBYOD.Feature.Navigation.Services;
using Sitecore.DependencyInjection;

namespace VRBYOD.Feature.Navigation
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITopLinksBuilder, TopLinksBuilder>();
            serviceCollection.AddTransient<INavigationBuilder, NavigationBuilder>();
        }
    }
}