using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Oniria.Core.Application.Contexts;
using Oniria.Core.Application.Features.Base;
using System.Reflection;

namespace Oniria.Core.Application.Extensions
{
    public static class DependencyInjectionApplicationLayer
    {
        public static void AddApplicationDependency(this IServiceCollection services)
        {
            // Injections
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserIncludeContext, UserIncludeContext>();
            services.AddScoped<IMediatorWrapper, MediatorWrapper>();
        }
    }
}
