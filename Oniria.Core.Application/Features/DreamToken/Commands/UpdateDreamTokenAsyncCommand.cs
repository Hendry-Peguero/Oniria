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

        public UpdateDreamTokenAsyncCommand(string id, CreateDreamTokenRequest request)
        {
            Id = id;
            Request = request;
        }
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

           
            var existingToken = await _dreamTokenRepository.GetByIdAsync(command.Id);
            if (existingToken == null)
            {
                result.AddError("DreamToken not found");
                return result;
            }

            
            var patientResult = await _mediator.Send(new GetPatientByIdAsyncQuery { Id = command.Request.PatientId });

            if (!patientResult.IsSuccess)
            {
                result.AddError(patientResult);
                return result;
            }

            
            _mapper.Map(command.Request, existingToken);
            existingToken.Id = command.Id; 

            try
            {
                await _dreamTokenRepository.UpdateAsync(existingToken);
                result.Data = existingToken;
            }
            catch (Exception ex)
            {
                result.AddError("Error updating DreamToken");
            }

            return result;
        }
    }
}
