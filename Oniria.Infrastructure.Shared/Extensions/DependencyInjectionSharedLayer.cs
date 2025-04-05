using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oniria.Core.Domain.Settings;
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
