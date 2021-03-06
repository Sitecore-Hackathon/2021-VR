using Microsoft.Extensions.DependencyInjection;
using VRBYOD.Foundation.Settings.Service;
using Sitecore.DependencyInjection;

namespace VRBYOD.Foundation.Settings
{
    public class ServiceConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISettingBuilder, SettingBuilder>();
        }
    }
}