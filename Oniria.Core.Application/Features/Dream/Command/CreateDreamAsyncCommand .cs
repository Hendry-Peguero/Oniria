using AutoMapper;
using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.Patient.Queries;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Core.Dtos.Dream.Request;

namespace Oniria.Core.Application.Features.Dream.Commands
{
    public class CreateDreamAsyncCommand : IRequest<OperationResult<DreamEntity>>
    {
        public CreateDreamRequest Request { get; set; }
    }

    public class CreateDreamAsyncCommandHandler : IRequestHandler<CreateDreamAsyncCommand, OperationResult<DreamEntity>>
    {
        private readonly IDreamRepository _dreamRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateDreamAsyncCommandHandler(
            IDreamRepository dreamRepository,
            IMediator mediator,
            IMapper mapper
        )
        {
            _dreamRepository = dreamRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<OperationResult<DreamEntity>> Handle(CreateDreamAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult<DreamEntity>.Create();
            var request = command.Request;

            var patientResult = await _mediator.Send(new GetPatientByIdAsyncQuery { Id = request.PatientId });

            if (!patientResult.IsSuccess)
            {
                result.AddError(patientResult);
                return result;
            }

            try
            {
                var dreamEntity = _mapper.Map<DreamEntity>(request);
                await _dreamRepository.CreateAsync(dreamEntity);
                result.Data = dreamEntity;
            }
            catch (Exception)
            {
                result.AddError("Error creating dream");
            }

            return result;
        }
    }
}
