using Oniria.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Repositories.SqlServer.Gender;
using Oniria.Infrastructure.Persistence.Repositories.SqlServer.EmotionalStates;
using Oniria.Infrastructure.Persistence.Repositories.SqlServer.Patient;
using Oniria.Infrastructure.Persistence.Repositories.SqlServer.Organization;


namespace Oniria.Infrastructure.Persistence.Extensions
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
            services.AddTransient<IEmotionalStatesRepository, EmotionalStatesRepository>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IOrganizationRepository, OrganizationRepository>();
        }
    }
}
