using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oniria.Infrastructure.Shared.Entities;
using System.Reflection;

namespace Oniria.Infrastructure.Shared.Extensions
{
    public static class DependencyInjectionSharedLayer
    {
        public static void AddSharedDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        }
    }
}
