using Oniria.Services;

namespace Oniria.Extensions
{
    public static class DependencyInjectionPresentationLayer
    {
        public static void AddPresentationDependency(this IServiceCollection services)
        {
            services.AddTransient<ISideMenuService, SideMenuService>();
        }
    }
}
