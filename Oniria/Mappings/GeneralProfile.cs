using AutoMapper;
using Oniria.Core.Dtos.Organization.Request;
using Oniria.Core.Dtos.Patient.Request;
using Oniria.Core.Dtos.User.Request;
using Oniria.ViewModels.Auth;

namespace Oniria.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            Auth();
        }

        private void Auth()
        {
            CreateMap<LoginViewModel, AuthenticationRequest>();
            CreateMap<RegisterPatientViewModel, RegisterPatientRequest>();
            CreateMap<RegisterOrganizationViewModel, RegisterOrganizationRequest>();
        }
    }
}
