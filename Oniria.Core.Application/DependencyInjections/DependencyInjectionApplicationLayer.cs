using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Oniria.Core.Application.DependencyInjection
{
    public static class DependencyInjectionApplicationLayer
    {
        public static void AddApplicationDependency(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
