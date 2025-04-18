using Microsoft.AspNetCore.Mvc;
using Oniria.Core.Domain.Enums;

namespace Oniria.Helpers
{
    public class Redirections
    {
        // Home
        public static RedirectToRouteResult HomeRedirection = new RedirectToRouteResult(new { controller = "Home", action = "HomeRedirection" });
        public static RedirectToRouteResult Home = new RedirectToRouteResult(new { controller = "Home", action = "Index" });
        public static RedirectToRouteResult UserDoctorHome = new RedirectToRouteResult(new { controller = "", action = "" });
        public static RedirectToRouteResult UserAssistanceHome = new RedirectToRouteResult(new { controller = "", action = "" });
        public static RedirectToRouteResult UserPatientHome = new RedirectToRouteResult(new { controller = "", action = "" });

        // HttpResponses
        public static RedirectToRouteResult Unauthorized = new RedirectToRouteResult(new { controller = "HttpResponse", action = "UnauthorizedView" });
        public static RedirectToRouteResult Forbidden = new RedirectToRouteResult(new { controller = "HttpResponse", action = "ForbiddenView" });


        public static RedirectToRouteResult GetHomeByUserRole(ActorsRoles? role)
        {
            switch (role)
            {
                case ActorsRoles.DOCTOR: return UserDoctorHome;
                case ActorsRoles.ASSISTANT: return UserAssistanceHome;
                case ActorsRoles.PATIENT: return UserPatientHome;
                default: return Home;
            }
        }
    }
}
