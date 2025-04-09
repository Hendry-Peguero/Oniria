using AutoMapper;
using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.Patient.Queries;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Core.Dtos.DreamToken.Request;

namespace Oniria.Core.Application.Features.DreamToken.Command
{
    public class CreateDreamTokenAsyncCommand : IRequest<OperationResult<DreamTokenEntity>>
    {
        public CreateDreamTokenRequest Request { get; set; }

       
    }

    public class CreateDreamTokenAsyncCommandHandler : IRequestHandler<CreateDreamTokenAsyncCommand, OperationResult<DreamTokenEntity>>
    {
        private readonly IDreamTokenRepository _dreamTokenRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateDreamTokenAsyncCommandHandler(
            IDreamTokenRepository dreamTokenRepository,
            IMediator mediator,
            IMapper mapper
        )
        {
            _dreamTokenRepository = dreamTokenRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<OperationResult<DreamTokenEntity>> Handle(CreateDreamTokenAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult<DreamTokenEntity>.Create();
            var request = command.Request;


            var patientResult = await _mediator.Send(new GetPatientByIdAsyncQuery { Id = request.PatientId });

            if (!patientResult.IsSuccess)
            {
                result.AddError(patientResult);
                return result;
            }

            try
            {
                var entity = _mapper.Map<DreamTokenEntity>(request);
                await _dreamTokenRepository.CreateAsync(entity);
                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.AddError("Error creating DreamToken");
            }

            return result;
        }
    }
}
