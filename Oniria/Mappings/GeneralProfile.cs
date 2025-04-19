using AutoMapper;
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
        }
    }
}
