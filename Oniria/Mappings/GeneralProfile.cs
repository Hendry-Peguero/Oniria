using AutoMapper;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Dtos.Employee.Request;
using Oniria.Core.Dtos.Organization.Request;
using Oniria.Core.Dtos.Patient.Request;
using Oniria.Core.Dtos.User.Request;
using Oniria.ViewModels.Auth;
using Oniria.ViewModels.Employee;
using Oniria.ViewModels.Organization;
using Oniria.ViewModels.Patient;

namespace Oniria.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            Auth();
            Patient();
            Employee();
            Organization();
        }

        private void Employee()
        {
            CreateMap<EmployeeEntity, EmployeeProfileViewModel>();
            CreateMap<EmployeeProfileViewModel, UpdateEmployeeRequest>();
        }

        private void Organization()
        {
            CreateMap<OrganizationEntity, OrganizationProfileViewModel>();
            CreateMap<OrganizationProfileViewModel, UpdateOrganizationRequest>();
        }

        private void Auth()
        {
            CreateMap<LoginViewModel, AuthenticationRequest>();
            CreateMap<RegisterPatientViewModel, RegisterPatientRequest>();
            CreateMap<RegisterOrganizationViewModel, RegisterOrganizationRequest>();
        }

        private void Patient()
        {
            CreateMap<CreatePatientByOrganizationViewModel, CreatePatientByOrganizationRequest>();
        }
    }
}
