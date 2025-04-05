using Microsoft.AspNetCore.Mvc;
using Oniria.Core.Domain.Enums;

namespace Oniria.Helpers
{
    public class Redirections
    {
        public static RedirectToRouteResult Home = new RedirectToRouteResult(new { controller = "Home", action = "Index" });
        public static RedirectToRouteResult UserDoctorHome = new RedirectToRouteResult(new { controller = "", action = "" });
        public static RedirectToRouteResult UserAssistanceHome = new RedirectToRouteResult(new { controller = "", action = "" });
        public static RedirectToRouteResult UserPatientHome = new RedirectToRouteResult(new { controller = "", action = "" });
        public static RedirectToRouteResult AccessDenied = new RedirectToRouteResult(new { controller = "", action = "" });
        public static RedirectToRouteResult Undefinied = new RedirectToRouteResult(new { controller = "", action = "" });


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
