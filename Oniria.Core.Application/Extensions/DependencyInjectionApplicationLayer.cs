using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Oniria.Core.Application.Extensions
{
    public static class DependencyInjectionApplicationLayer
    {
        public static void AddApplicationDependency(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
