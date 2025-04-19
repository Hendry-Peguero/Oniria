using FluentValidation;
using FluentValidation.AspNetCore;
using NToastNotify;
using Oniria.Services;
using System.Reflection;

namespace Oniria.Extensions
{
    public static class DependencyInjectionPresentationLayer
    {
        public static void AddPresentationDependency(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services
                .AddControllersWithViews()
                .AddNToastNotifyToastr(new ToastrOptions()
                {
                    PositionClass = ToastPositions.BottomRight
                });
            services.AddSession();
            services.AddHttpClient();
            services.AddTransient<ISideMenuService, SideMenuService>();
            services.AddTransient<IBackgroundService, Oniria.Services.BackgroundService>();
            services.AddScoped<IUserContextService, UserContextService>();
        }
    }
}
