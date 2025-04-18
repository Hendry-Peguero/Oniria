using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Infrastructure.Persistence.Contexts;
using Oniria.Infrastructure.Persistence.Repositories;
using Oniria.Infrastructure.Persistence.Repositories.Base;
using Oniria.Infrastructure.Persistence.Repositories.SqlServer.Dream;
using Oniria.Infrastructure.Persistence.Repositories.SqlServer.DreamAnalysis;
using Oniria.Infrastructure.Persistence.Repositories.SqlServer.EmotionalStates;
using Oniria.Infrastructure.Persistence.Repositories.SqlServer.Gender;
using Oniria.Infrastructure.Persistence.Repositories.SqlServer.Membership;
using Oniria.Infrastructure.Persistence.Repositories.SqlServer.MembershipAcquisition;
using Oniria.Infrastructure.Persistence.Repositories.SqlServer.MembershipBenefit;
using Oniria.Infrastructure.Persistence.Repositories.SqlServer.MembershipBenefitRelation;
using Oniria.Infrastructure.Persistence.Repositories.SqlServer.MembershipCategory;
using Oniria.Infrastructure.Persistence.Repositories.SqlServer.Organization;
using Oniria.Infrastructure.Persistence.Repositories.SqlServer.Patient;


namespace Oniria.Infrastructure.Persistence.Extensions
{
    public static class DependencyInjectionPersistenceLayer
    {
        public static void AddPersistenceDependency(this IServiceCollection services, IConfiguration configuration)
        {
            // For Migrations
            var connectionString = configuration.GetConnectionString("SqlServerConnection");
            services.AddDbContext<ApplicationContext>(
                options => options.UseSqlServer(
                    connectionString,
                    m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)
                )
            );

            // Repositories
            services.AddTransient<IDreamAnalysisRepository, DreamAnalysisRepository>();
            services.AddTransient<IDreamRepository, DreamRepository>();
            services.AddTransient<IDreamTokenRepository, DreamTokenRepository>();
            services.AddTransient<IEmotionalStatesRepository, EmotionalStatesRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IGenderRepository, GenderRepository>();
            services.AddTransient<IMembershipAcquisitionRepository, MembershipAcquisitionRepository>();
            services.AddTransient<IMembershipBenefitRelationRepository, MembershipBenefitRelationRepository>();
            services.AddTransient<IMembershipBenefitRepository, MembershipBenefitRepository>();
            services.AddTransient<IMembershipCategoryRepository, MembershipCategoryRepository>();
            services.AddTransient<IMembershipRepository, MembershipRepository>();
            services.AddTransient<IOrganizationRepository, OrganizationRepository>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddScoped(typeof(DbSetWrapper<>));
        }
    }
}
