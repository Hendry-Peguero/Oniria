using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.Employee.Queries;
using Oniria.Core.Application.Features.Organization.Queries;
using Oniria.Core.Application.Features.Patient.Queries;
using Oniria.Core.Application.Features.User.Queries;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Dtos.User.Response;

namespace Oniria.Services
{
    public class UserContextService : IUserContextService
    {
        // Utils
        private readonly IMediatorWrapper mediator;

        // Context
        public UserResponse? LoggedUser;
        public PatientEntity? UserPatientInfo;
        public EmployeeEntity? UserEmployeeInfo;
        public OrganizationEntity? EmployeeOrganizationInfo;


        public UserContextService(IMediatorWrapper mediator)
        {
            this.mediator = mediator;
        }

        public async Task<UserResponse?> GetLoggedUser()
        {
            var userLogged = await mediator.Send(new GetUserSessionAsyncQuery());

            if (!userLogged.IsSuccess || userLogged.Data == null)
            {
                CleanContext();
                return null;
            }

            if (LoggedUser != null)
            {
                return LoggedUser;
            }

            return LoggedUser = userLogged.Data;
        }

        public async Task<PatientEntity?> GetUserPatientInfo()
        {
            var userLogged = await GetLoggedUser();

            if (userLogged == null)
            {
                return null;
            }

            if (UserPatientInfo != null)
            {
                return UserPatientInfo;
            }

            var userPatientInfo = (await mediator.Send(new GetAllPatientAsyncQuery()))
                .Data!
                .FirstOrDefault(x => x.UserId == userLogged.Id);

            return UserPatientInfo = userPatientInfo;
        }

        public async Task<EmployeeEntity?> GetUserEmployeeInfo()
        {
            var userLogged = await GetLoggedUser();

            if (userLogged == null)
            {
                return null;
            }

            if (UserEmployeeInfo != null)
            {
                return UserEmployeeInfo;
            }

            var userEmployeeInfo = (await mediator.Send(new GetAllEmployeeAsyncQuery(), 
                    emp => emp.OrganizationWhereWork!
                ))
                .Data!
                .FirstOrDefault(x => x.UserId == userLogged.Id);

            return UserEmployeeInfo = userEmployeeInfo;
        }

        public async Task<OrganizationEntity?> GetEmployeeOrganizationInfo()
        {
            var userLogged = await GetLoggedUser();

            if (userLogged == null)
            {
                return null;
            }

            if (EmployeeOrganizationInfo != null)
            {
                return EmployeeOrganizationInfo;
            }

            var userEmployeeInfo = await GetUserEmployeeInfo();

            if (userEmployeeInfo == null)
            {
                return null;
            }

            return EmployeeOrganizationInfo = userEmployeeInfo.OrganizationWhereWork;
        }


        public void CleanContext()
        {
            LoggedUser = null;
            UserPatientInfo = null;
            UserEmployeeInfo = null;
            EmployeeOrganizationInfo = null;
        }
    }

    public interface IUserContextService
    {
        Task<UserResponse?> GetLoggedUser();
        Task<PatientEntity?> GetUserPatientInfo();
        Task<EmployeeEntity?> GetUserEmployeeInfo();
        Task<OrganizationEntity?> GetEmployeeOrganizationInfo();
    }
}
