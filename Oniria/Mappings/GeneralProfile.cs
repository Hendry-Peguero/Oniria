using AutoMapper;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Dtos.Dream.Request;
using Oniria.Core.Dtos.Employee.Request;
using Oniria.Core.Dtos.Organization.Reponse;
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
            Organization();
            Dream();
            Patient();
            Employee();
        }

        private void Employee()
        {
            CreateMap<EmployeeEntity, EmployeeProfileViewModel>();
            CreateMap<EmployeeProfileViewModel, UpdateEmployeeRequest>();
            CreateMap<CreateEmployeeByOrganizationViewModel, CreateEmployeeByOrganizationRequest>();
        }

        private void Organization()
        {
            CreateMap<OrganizationEntity, OrganizationProfileViewModel>();
            CreateMap<OrganizationProfileViewModel, UpdateOrganizationRequest>();

            CreateMap<DashboardResponse, DashboardViewModel>();
            CreateMap<PatientDreamCountResponse, PatientDreamCountViewModel>();
            CreateMap<RecentPatientResponse, RecentPatientViewModel>();
            CreateMap<RecentDreamResponse, RecentDreamViewModel>();
            CreateMap<RecentEmployeeResponse, RecentEmployeeViewModel>();
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

            CreateMap<PatientEntity, PatientProfileViewModel>()
                .ForMember(x => x.Genders, opt => opt.Ignore());

            CreateMap<PatientProfileViewModel, UpdatePatientRequest>();
        }

        private void Dream()
        {
            CreateMap<CreateDreamRequest, DreamEntity>();
            CreateMap<RegisterDreamViewModel, RegisterDreamRequest>();
        }
    }
}
