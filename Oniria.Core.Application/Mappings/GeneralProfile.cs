using AutoMapper;
using Oniria.Core.Application.Extensions;
using Oniria.Core.Application.Helpers;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Enums;
using Oniria.Core.Dtos.Organization.Request;
using Oniria.Core.Dtos.Patient.Request;

namespace Oniria.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            Patient();
            Organization();
        }

        private void Patient()
        {
            CreateMap<PatientEntity, CreatePatientRequest>()
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.MapFrom(s => GeneratorHelper.GuidString()))
                .ForMember(p => p.Status, opt => opt.MapFrom(s => StatusEntity.ACTIVE))
                .IgnoreAuditMembers();

            CreateMap<PatientEntity, UpdatePatientRequest>()
                .ReverseMap()
                .IgnoreTimeStampsAuditMembers();
        }

        private void Organization()
        {
            CreateMap<OrganizationEntity, CreateOrganizationRequest>()
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.MapFrom(s => GeneratorHelper.GuidString()))
                .ForMember(p => p.Status, opt => opt.MapFrom(s => StatusEntity.ACTIVE))
                .IgnoreAuditMembers();

            CreateMap<OrganizationEntity, UpdateOrganizationRequest>()
                .ReverseMap()
                .IgnoreTimeStampsAuditMembers();
        }
    }
}
