using Microsoft.AspNetCore.Mvc;
using Oniria.Core.Domain.Enums;

namespace Oniria.Helpers
{
    public class Redirections
    {
        // Home
        public static RedirectToRouteResult HomeRedirection = new RedirectToRouteResult(new { controller = "Home", action = "HomeRedirection" });
        public static RedirectToRouteResult Home = new RedirectToRouteResult(new { controller = "Home", action = "Index" });
        public static RedirectToRouteResult UserDoctorHome = new RedirectToRouteResult(new { controller = "Organization", action = "Dashboard" });
        public static RedirectToRouteResult UserAssistanceHome = new RedirectToRouteResult(new { controller = "Employee", action = "Profile" });
        public static RedirectToRouteResult UserPatientHome = new RedirectToRouteResult(new { controller = "Patient", action = "RegisterDream" });

        // Auth
        public static RedirectToRouteResult Login = new RedirectToRouteResult(new { controller = "Auth", action = "Login" });

        // Organization
        public static RedirectToRouteResult OrganizationProfile = new RedirectToRouteResult(new { controller = "Organization", action = "Profile" });

        // Employee
        public static RedirectToRouteResult EmployeeProfile = new RedirectToRouteResult(new { controller = "Employee", action = "Profile" });

        //Patient
        public static RedirectToRouteResult PatientProfile = new RedirectToRouteResult(new { controller = "Patient", action = "Profile" });

        // Dream
        public static RedirectToRouteResult RegisterDream = new RedirectToRouteResult(new { controller = "Patient", action = "RegisterDream" });


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
