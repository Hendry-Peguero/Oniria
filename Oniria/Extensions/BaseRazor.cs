using Microsoft.AspNetCore.Mvc.Razor;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Dtos.User.Response;
using Oniria.Services;

namespace Oniria.Extensions
{
    public abstract class BaseRazor : RazorPage<dynamic>
    {
        private IUserContextService? _userContextService;
        private IUserContextService UserContextService =>
            _userContextService ??= Context.RequestServices.GetService<IUserContextService>()!;


        public async Task<UserResponse?> GetLoggedUserAsync()
        {
            if (UserContextService == null) return null;
            return await UserContextService.GetLoggedUser();
        }

        public async Task<PatientEntity?> GetUserPatientInfoAsync()
        {
            if (UserContextService == null) return null;
            return await UserContextService.GetUserPatientInfo();
        }

        public async Task<EmployeeEntity?> GetUserEmployeeInfoAsync()
        {
            if (UserContextService == null) return null;
            return await UserContextService.GetUserEmployeeInfo();
        }

        public async Task<OrganizationEntity?> GetEmployeeOrganizationInfoAsync()
        {
            if (UserContextService == null) return null;
            return await UserContextService.GetEmployeeOrganizationInfo();
        }
    }
}
