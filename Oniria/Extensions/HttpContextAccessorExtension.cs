namespace Oniria.Extensions
{
    public static class HttpContextAccessorExtension
    {
        public struct RouteData
        {
            public string Controller { get; set; }
            public string Action { get; set; }

        }

        public static RouteData GetRouteInfo(this IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext;

            return new RouteData
            {
                Controller = httpContext?.GetRouteValue("controller")?.ToString() ?? "",
                Action = httpContext?.GetRouteValue("action")?.ToString() ?? ""
            };
        }
    }
}
