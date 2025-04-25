using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.Patient.Queries;
using Oniria.Core.Dtos.Organization.Reponse;

namespace Oniria.Core.Application.Features.Organization.Queries
{
    public class GetPatientTrakingAsyncQuery : IRequest<OperationResult<PatientTrackingResponse>>
    {
        public string PatientId { get; set; }
    }

    public class GetPatientTrakingAsyncQueryHandler : IRequestHandler<GetPatientTrakingAsyncQuery, OperationResult<PatientTrackingResponse>>
    {
        private readonly IMediatorWrapper mediator;

        public GetPatientTrakingAsyncQueryHandler(IMediatorWrapper mediator)
        {
            this.mediator = mediator;
        }

        public async Task<OperationResult<PatientTrackingResponse>> Handle(GetPatientTrakingAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<PatientTrackingResponse>.Create();

            var patientResult = await mediator.Send(
                new GetPatientByIdAsyncQuery { Id = request.PatientId },
                p => p.Dreams,
                p => p.Gender
            );

            if (!patientResult.IsSuccess)
            {
                result.AddError(patientResult);
                return result;
            }

            var patient = patientResult.Data;

            var dreams = patient!.Dreams
                ?.OrderByDescending(d => d.CreatedOn)
                .ToList() ?? new();

            var dreamDetails = dreams
                .Select(d => new PatientDreamResponse
                {
                    Date = d.CreatedOn,
                    Title = d.Title,
                    Summary = d.Prompt?.Length > 100 ? d.Prompt.Substring(0, 100) + "..." : d.Prompt ?? ""
                })
                .ToList();

            var dreamsByDate = dreams
                .Where(d => d.CreatedOn.Date >= DateTime.UtcNow.AddDays(-30))
                .GroupBy(d => d.CreatedOn.Date)
                .ToDictionary(g => g.Key, g => g.Count());

            var lastDreamDate = dreams.FirstOrDefault()?.CreatedOn;

            var response = new PatientTrackingResponse
            {
                FullName = $"{patient.Name} {patient.LastName}",
                BornDate = patient.BornDate,
                Gender = patient.Gender?.Description ?? "No especificado",
                PhoneNumber = patient.PhoneNumber,
                Address = patient.Address,
                TotalDreams = dreams.Count,
                Dreams = dreamDetails,
                DreamsByDateList = dreamsByDate
                .Select(x => new DreamDateCountResponse
                {
                    Date = x.Key.ToString("yyyy-MM-dd"),
                    Count = x.Value
                })
                .ToList(),
                HasDreamsInLast30Days = dreams.Any(d => d.CreatedOn >= DateTime.UtcNow.AddDays(-30)),
                LastDreamDate = lastDreamDate
            };

            result.Data = response;

            return result;
        }
    }

}
