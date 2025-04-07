using Microsoft.AspNetCore.Http;

namespace Oniria.Core.Application.Extensions
{
    public static class HttpContextAccessorExtension
    {
        public static string GetOrigin(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.HttpContext?.Request.Headers["Origin"].FirstOrDefault() ?? "";
        }
    }
}
