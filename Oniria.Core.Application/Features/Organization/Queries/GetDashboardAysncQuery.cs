using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.Dream.Queries;
using Oniria.Core.Application.Features.Employee.Queries;
using Oniria.Core.Application.Features.Patient.Queries;
using Oniria.Core.Dtos.Organization.Reponse;

namespace Oniria.Core.Application.Features.Organization.Queries
{
    public class GetDashboardAsyncQuery : IRequest<OperationResult<DashboardResponse>> 
    { 
        public string OrganizationId { get; set; }
    }

    public class GetDashboardAsyncQueryHandler : IRequestHandler<GetDashboardAsyncQuery, OperationResult<DashboardResponse>>
    {
        private readonly IMediatorWrapper mediator;

        public GetDashboardAsyncQueryHandler(IMediatorWrapper mediator)
        {
            this.mediator = mediator;
        }

        public async Task<OperationResult<DashboardResponse>> Handle(GetDashboardAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<DashboardResponse>.Create();

            var patientResult = await mediator.Send(
                new GetAllPatientAsyncQuery(),
                p => p.Gender,
                p => p.Dreams
            );
            var employeeResult = await mediator.Send(
                new GetAllEmployeeAsyncQuery(),
                emp => emp.OrganizationWhereWork
            );
            var dreamResult = await mediator.Send(
                new GetAllDreamAsyncQuery(),
                d => d.Patient
            );

            var patients = patientResult
                .Data!
                .Where(p => p.OrganizationId == request.OrganizationId)
                .ToList();

            var employees = employeeResult
                .Data!
                .Where(e => e.OrganizationWhereWork.Id == request.OrganizationId)
                .ToList();
;
            var dreams = dreamResult
                .Data!
                .Where(d => d.Patient.OrganizationId == request.OrganizationId)
                .ToList();

            var dreamsByDate = dreams
                .GroupBy(d => d.CreatedOn.Date)
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Count());

            var today = DateTime.UtcNow.Date;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            var startOfMonth = new DateTime(today.Year, today.Month, 1);

            var dashboard = new DashboardResponse
            {
                OrgnanizationId = patients.FirstOrDefault()?.OrganizationId ?? "",

                TotalPatients = patients.Count,
                TotalEmployees = employees.Count,
                TotalDreams = dreams.Count,
                DreamsToday = dreams.Count(d => d.CreatedOn.Date == today),
                DreamsThisWeek = dreams.Count(d => d.CreatedOn.Date >= startOfWeek),
                DreamsThisMonth = dreams.Count(d => d.CreatedOn.Date >= startOfMonth),
                DreamsByDate = dreamsByDate,

                TopPatientsByDreams = patients
                    .Select(p => new PatientDreamCountResponse
                    {
                        FullName = $"{p.Name} {p.LastName}",
                        DreamCount = p.Dreams?.Count ?? 0
                    })
                    .OrderByDescending(p => p.DreamCount)
                    .Take(5)
                    .ToList(),

                PatientsByAgeGroup = patients
                    .GroupBy(p => GetAgeGroup(GetAge(p.BornDate)))
                    .ToDictionary(g => g.Key, g => g.Count()),

                PatientsByGender = patients
                    .GroupBy(p => p.Gender?.Description ?? "Unknown")
                    .ToDictionary(g => g.Key, g => g.Count()),

                RecentPatients = patients
                    .OrderByDescending(p => p.CreatedOn)
                    .Take(5)
                    .Select(p => new RecentPatientResponse
                    {
                        FullName = $"{p.Name} {p.LastName}",
                        RegisteredDate = p.CreatedOn
                    })
                    .ToList(),

                RecentDreams = dreams
                    .OrderByDescending(d => d.CreatedOn)
                    .Take(5)
                    .Select(d => new RecentDreamResponse
                    {
                        PatientName = $"{d.Patient?.Name} {d.Patient?.LastName}",
                        DreamDate = d.CreatedOn,
                        ShortDescription = d.Prompt?.Length > 50 ? d.Prompt.Substring(0, 50) + "..." : d.Prompt ?? ""
                    })
                    .ToList(),

                PatientsWithoutDreamsIn30Days = patients
                    .Count(p =>
                        !(p.Dreams?.Any(d => d.CreatedOn >= DateTime.UtcNow.AddDays(-30)) ?? false))
            };

            result.Data = dashboard;
            return result;
        }

        private static int GetAge(DateTime birthDate)
        {
            var today = DateTime.UtcNow;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }

        private static string GetAgeGroup(int age)
        {
            return age switch
            {
                < 13 => "Child",
                < 20 => "Teen",
                < 35 => "Young Adult",
                < 60 => "Adult",
                _ => "Senior"
            };
        }
    }
}

