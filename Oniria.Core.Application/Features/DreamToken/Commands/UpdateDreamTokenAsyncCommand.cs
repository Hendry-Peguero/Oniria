using AutoMapper;
using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.Patient.Queries;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Core.Dtos.DreamToken.Request;

namespace Oniria.Core.Application.Features.DreamToken.Command
{
    public class UpdateDreamTokenAsyncCommand : IRequest<OperationResult<DreamTokenEntity>>
    {
        public string Id { get; set; }
        public CreateDreamTokenRequest Request { get; set; }
    }

    public class UpdateDreamTokenAsyncCommandHandler : IRequestHandler<UpdateDreamTokenAsyncCommand, OperationResult<DreamTokenEntity>>
    {
        private readonly IDreamTokenRepository _dreamTokenRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpdateDreamTokenAsyncCommandHandler(
            IDreamTokenRepository dreamTokenRepository,
            IMediator mediator,
            IMapper mapper
        )
        {
            _dreamTokenRepository = dreamTokenRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<OperationResult<DreamTokenEntity>> Handle(UpdateDreamTokenAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult<DreamTokenEntity>.Create();
            var request = command.Request;

            
            var dreamTokenToUpdate = await _dreamTokenRepository.GetByIdAsync(command.Id);
            if (dreamTokenToUpdate == null)
            {
                result.AddError("DreamToken not found");
                return result;
            }

            
            var patientResult = await _mediator.Send(new GetPatientByIdAsyncQuery { Id = request.PatientId });
            if (!patientResult.IsSuccess)
            {
                result.AddError(patientResult);
                return result;
            }

            
            dreamTokenToUpdate = _mapper.Map<DreamTokenEntity>(request);
            

            try
            {
                await _dreamTokenRepository.UpdateAsync(dreamTokenToUpdate);
            }
            catch (Exception ex)
            {
                result.AddError("Patient could not be updated");
                return result;
            }

            result.Data = dreamTokenToUpdate;

            return result;

          
        }
    }
}
