using AutoMapper;
using Oniria.Core.Domain.Enums;
using Oniria.Core.Dtos.User.Request;
using Oniria.Core.Dtos.User.Response;
using Oniria.Infrastructure.Identity.Entities;

namespace Oniria.Infrastructure.Identity.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<ApplicationUser, UserResponse>()
                    .ForMember(dest => dest.Roles, opt => opt.Ignore())
                    .ForMember(dest => dest.Password, opt => opt.MapFrom(s => s.PasswordHash))
                    .ReverseMap()
                    .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore())
                    .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                    .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
                    .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
                    .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
                    .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
                    .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
                    .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(s => s.Password))
                    .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
                    .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
                    .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore());

            CreateMap<ApplicationUser, CreateUserRequest>()
                    .ForMember(dest => dest.Password, opt => opt.MapFrom(s => s.PasswordHash))
                    .ReverseMap()
                    .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore())
                    .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                    .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
                    .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
                    .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
                    .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
                    .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
                    .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(s => s.Password))
                    .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.MapFrom(s => true))
                    .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
                    .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(s => StatusEntity.ACTIVE));

            CreateMap<ApplicationUser, UpdateUserRequest>()
                    .ForMember(dest => dest.Password, opt => opt.MapFrom(s => s.PasswordHash))
                    .ReverseMap()
                    .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore())
                    .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                    .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
                    .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
                    .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
                    .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
                    .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
                    .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(s => s.Password))
                    .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
                    .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
                    .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore());
        }
    }
}
