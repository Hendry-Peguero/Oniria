using Oniria.Core.Domain.Enums;
using Oniria.Core.Dtos.User.Response;
using Oniria.Extensions;

namespace Oniria.Services
{
    public class Option
    {
        public string Text { get; set; }
        public string BootstrapIcon { get; set; }
        public bool Selected { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }

    public struct Section
    {
        public string Text { get; set; }
    }

    public class SideMenuService : ISideMenuService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserContextService userContext;

        public SideMenuService(
            IHttpContextAccessor httpContextAccessor,
            IUserContextService userContext
        )
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userContext = userContext;
        }

        public async Task<List<dynamic>> GetOptions()
        {
            var options = new List<dynamic>();
            var loggedUser = await userContext.GetLoggedUser();

            if (loggedUser != null)
            {
                switch (loggedUser.Roles!.FirstOrDefault())
                {
                    case ActorsRoles.PATIENT: options = GetPatientOptions(); break;
                    case ActorsRoles.DOCTOR: options = GetDoctorOptions(); break;
                    case ActorsRoles.ASSISTANT: options = GetAssistanceOptions(); break;
                }

                var routeInfo = httpContextAccessor.GetRouteInfo();
                var optionToSelect = options.FirstOrDefault(x =>
                    x is Option &&
                    x.Controller == routeInfo.Controller &&
                    x.Action == routeInfo.Action
                );

                if (optionToSelect != null) optionToSelect.Selected = true;
            }

            return await FilterOptions(options, loggedUser);
        }

        private async Task<List<dynamic>> FilterOptions(List<dynamic> options, UserResponse? loggedUser)
        {
            if (loggedUser != null)
            {
                switch (loggedUser.Roles!.FirstOrDefault())
                {
                    case ActorsRoles.DOCTOR:
                        {
                            var organization = await userContext.GetEmployeeOrganizationInfo();

                            if (organization == null)
                            {
                                options = options.Where(o =>
                                    o.Text != "Organization Profile" &&
                                    o.Text != "Change Membership"
                                ).ToList();
                            }

                            break;
                        }
                }
            }

            return options;
        }

        private List<dynamic> GetPatientOptions()
        {
            return new List<dynamic> {
                new Section {
                    Text = "Dream"
                },
                new Option {
                    Text = "Register Dream",
                    BootstrapIcon = "bi-moon-stars-fill",
                    Controller = "Patient",
                    Action = "RegisterDream"
                },
                new Option {
                    Text = "Dream Record",
                    BootstrapIcon = "bi-folder-fill",
                    Controller = "Patient",
                    Action = "DreamRecords"
                },
                new Option {
                    Text = "Dream Analisys Record",
                    BootstrapIcon = "bi-pie-chart-fill",
                    Controller = "Patient",
                    Action = "DreamAnalisysRecords"
                },
                new Section {
                    Text = "Configs"
                },
                new Option {
                    Text = "Profile",
                    BootstrapIcon = "bi-person-bounding-box",
                    Controller = "Patient",
                    Action = "Profile"
                },
                new Option {
                    Text = "Change Membership",
                    BootstrapIcon = "bi-credit-card-2-back-fill",
                    Controller = "Membership",
                    Action = "ChangePatientMembership"
                },
            };
        }

        private List<dynamic> GetDoctorOptions()
        {
            return new List<dynamic> {
                new Section {
                    Text = "General"
                },
                new Option {
                    Text = "Dashboard",
                    BootstrapIcon = "bi-speedometer2",
                    Controller = "Organization",
                    Action = "Dashboard"
                },
                new Section {
                    Text = "Patients"
                },
                new Option {
                    Text = "Create Patient",
                    BootstrapIcon = "bi-person-plus-fill",
                    Controller = "Patient",
                    Action = "CreatePatientByOrganization"
                },
                new Option {
                    Text = "Patient Records",
                    BootstrapIcon = "bi-person-lines-fill",
                    Controller = "Organization",
                    Action = "PatientRecords"
                },
                new Option {
                    Text = "Patient Tracking",
                    BootstrapIcon = "bi-graph-up-arrow",
                    Controller = "Organization",
                    Action = "PatientTracking"
                },
                new Section {
                    Text = "Employees"
                },
                new Option {
                    Text = "Create Employee",
                    BootstrapIcon = "bi-person-fill-gear",
                    Controller = "Employee",
                    Action = "CreateEmployeeByOrganization"
                },
                new Section {
                    Text = "Organization"
                },
                new Option {
                    Text = "Employee Records",
                    BootstrapIcon = "bi-person-lines-fill",
                    Controller = "Organization",
                    Action = "EmployeeRecords"
                },
                new Option {
                    Text = "Organization Profile",
                    BootstrapIcon = "bi-building-fill",
                    Controller = "Organization",
                    Action = "Profile"
                },
                new Option {
                    Text = "Change Membership",
                    BootstrapIcon = "bi-credit-card-2-back-fill",
                    Controller = "Membership",
                    Action = "ChangeOrganizationMembership"
                },
                new Section {
                    Text = "Configs"
                },
                new Option {
                    Text = "Profile",
                    BootstrapIcon = "bi-person-vcard-fill",
                    Controller = "Employee",
                    Action = "Profile"
                }
            };
        }

        private List<dynamic> GetAssistanceOptions()
        {
            return new List<dynamic> {
                new Section {
                    Text = "Patients"
                },
                new Option {
                    Text = "Create Patient",
                    BootstrapIcon = "bi-person-plus-fill",
                    Controller = "Patient",
                    Action = "CreatePatientByOrganization"
                },
                new Section {
                    Text = "Configs"
                },
                new Option {
                    Text = "Profile",
                    BootstrapIcon = "bi-person-vcard-fill",
                    Controller = "Employee",
                    Action = "Profile"
                }
            };
        }

    }


    public interface ISideMenuService
    {
        Task<List<dynamic>> GetOptions();
    }
}
