using DreamHouse.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oniria.Core.Application.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Repositories.SqlServer.Gender;


namespace Oniria.Infrastructure.Persistence.DependencyInjection
{
    public static class DependencyInjectionPersistenceLayer
    {
        public static void AddPersistenceDependency(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServerConnection");

            services.AddDbContext<ApplicationContext>(
                options => options.UseSqlServer(
                    connectionString,
                    m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)
                )
            );


            // Repositories
            services.AddTransient<IGenderRepository, GenderRepository>();
        }
    }
}
