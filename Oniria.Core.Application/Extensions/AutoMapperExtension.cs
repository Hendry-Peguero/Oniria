using AutoMapper;

namespace Oniria.Core.Application.Extensions
{
    public static class AutoMapperExtension
    {
        public static IMappingExpression<TSource, TDestination> IgnoreTimeStampsAuditMembers<TSource, TDestination>
        (this IMappingExpression<TSource, TDestination> mapping)
        {
            return mapping
                .ForMember("CreatedOn", opt => opt.Ignore())
                .ForMember("UpdatedOn", opt => opt.Ignore())
                .ForMember("DeletedOn", opt => opt.Ignore());
        }

        public static IMappingExpression<TSource, TDestination> IgnoreAuditMembers<TSource, TDestination>
        (this IMappingExpression<TSource, TDestination> mapping)
        {
            return IgnoreTimeStampsAuditMembers(mapping)
                .ForMember("Status", opt => opt.Ignore());
        }
    }
}
