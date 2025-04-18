using NToastNotify;
using Oniria.Services;

namespace Oniria.Extensions
{
    public static class DependencyInjectionPresentationLayer
    {
        public static void AddPresentationDependency(this IServiceCollection services)
        {
            services.AddControllersWithViews().AddNToastNotifyToastr(
                new ToastrOptions()
                {
                    PositionClass = ToastPositions.BottomRight
                }
            );
            services.AddSession();
            services.AddHttpClient();
            services.AddTransient<ISideMenuService, SideMenuService>();
            services.AddScoped<IUserContextService, UserContextService>();
        }
    }
}
