using AutoMapper;
using Oniria.Core.Application.Extensions;
using Oniria.Core.Application.Helpers;
using Oniria.Core.Domain.Constants;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Enums;
using Oniria.Core.Dtos.Dream.Request;
using Oniria.Core.Dtos.DreamAnalsys.Request;
using Oniria.Core.Dtos.DreamToken.Request;
using Oniria.Core.Dtos.Employee.Request;
using Oniria.Core.Dtos.MembershipAcquisition.Request;
using Oniria.Core.Dtos.Organization.Request;
using Oniria.Core.Dtos.Patient.Request;
using Oniria.Core.Dtos.User.Request;

namespace Oniria.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            Patient();
            Organization();
            Employee();
            Dream();
            DreamAnalysis();
            DreamToken();
            MembershipAcquisition();
        }

        private void Patient()
        {
            CreateMap<PatientEntity, CreatePatientRequest>()
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.MapFrom(s => GeneratorHelper.GuidString()))
                .ForMember(p => p.Status, opt => opt.MapFrom(s => StatusEntity.ACTIVE))
                .IgnoreTimeStampsAuditMembers();

            CreateMap<PatientEntity, UpdatePatientRequest>()
                .ReverseMap()
                .IgnoreTimeStampsAuditMembers();

            CreateMap<RegisterPatientRequest, CreateUserRequest>();

            CreateMap<RegisterPatientRequest, CreatePatientRequest>()
                .ForMember(rp => rp.UserId, opt => opt.Ignore())
                .ForMember(rp => rp.OrganizationId, opt => opt.MapFrom(cp => OrganizationIdsConstants.OrphanedOrganization));

            CreateMap<CreatePatientByOrganizationRequest, CreateUserRequest>();

            CreateMap<CreatePatientByOrganizationRequest, CreatePatientRequest>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

        }

        private void Organization()
        {
            CreateMap<OrganizationEntity, CreateOrganizationRequest>()
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.MapFrom(s => GeneratorHelper.GuidString()))
                .ForMember(p => p.Status, opt => opt.MapFrom(s => StatusEntity.ACTIVE))
                .IgnoreTimeStampsAuditMembers();

            CreateMap<OrganizationEntity, UpdateOrganizationRequest>()
                .ReverseMap()
                .IgnoreTimeStampsAuditMembers();

            CreateMap<RegisterOrganizationRequest, CreateUserRequest>();

            CreateMap<RegisterOrganizationRequest, CreateEmployeeRequest>()
                .ForMember(dest => dest.Dni, opt => opt.MapFrom(src => src.EmployeeDni))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EmployeeName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.EmployeeLastName))
                .ForMember(dest => dest.BornDate, opt => opt.MapFrom(src => src.EmployeeBornDate))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.EmployeePhoneNumber))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.EmployeeAddress))
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.OrganizationId, opt => opt.Ignore());

            CreateMap<RegisterOrganizationRequest, CreateOrganizationRequest>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OrganizationName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.OrganizationAddress))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.OrganizationPhoneNumber))
                .ForMember(dest => dest.EmployeeOwnerId, opt => opt.Ignore());
        }

        private void Employee()
        {
            CreateMap<EmployeeEntity, CreateEmployeeRequest>()
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.MapFrom(s => GeneratorHelper.GuidString()))
                .ForMember(p => p.Status, opt => opt.MapFrom(s => StatusEntity.ACTIVE))
                .IgnoreTimeStampsAuditMembers();

            CreateMap<EmployeeEntity, UpdateEmployeeRequest>()
                .ReverseMap()
                .IgnoreTimeStampsAuditMembers();
        }

        private void DreamAnalysis()
        {
            CreateMap<DreamAnalysisEntity, CreateDreamAnalysisRequest>()
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.MapFrom(s => GeneratorHelper.GuidString()))
                .ForMember(p => p.Status, opt => opt.MapFrom(s => StatusEntity.ACTIVE))
                .IgnoreTimeStampsAuditMembers();
        }


        private void DreamToken()
        {
            CreateMap<DreamTokenEntity, CreateDreamTokenRequest>()
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.MapFrom(s => GeneratorHelper.GuidString()))
                .ForMember(p => p.Status, opt => opt.MapFrom(s => StatusEntity.ACTIVE))
                .IgnoreTimeStampsAuditMembers();

            CreateMap<DreamTokenEntity, UpdateDreamTokenRequest>()
                .ReverseMap()
                .IgnoreTimeStampsAuditMembers();
        }

        private void MembershipAcquisition()
        {
            CreateMap<MembershipAcquisitionEntity, CreateMembershipAcquisitionRequest>()
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.MapFrom(s => GeneratorHelper.GuidString()))
                .ForMember(p => p.Status, opt => opt.MapFrom(s => StatusEntity.ACTIVE))
                .IgnoreTimeStampsAuditMembers();
        }

        private void Dream()
        {
            CreateMap<CreateDreamRequest, DreamEntity>()
                .ForMember(p => p.Id, opt => opt.MapFrom(s => GeneratorHelper.GuidString()))
                .ForMember(p => p.Status, opt => opt.MapFrom(s => StatusEntity.ACTIVE))
                .IgnoreTimeStampsAuditMembers();
        }
    }
}
