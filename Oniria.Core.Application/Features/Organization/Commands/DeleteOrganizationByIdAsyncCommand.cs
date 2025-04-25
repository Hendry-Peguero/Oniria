using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.Organization.Queries;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.Organization.Commands
{
    public class DeleteOrganizationByIdAsyncCommand : IRequest<OperationResult>
    {
        public string Id { get; set; }
    }

    public class DeleteOrganizationByIdAsyncCommandHandler : IRequestHandler<DeleteOrganizationByIdAsyncCommand, OperationResult>
    {
        private readonly IOrganizationRepository organizationRepository;
        private readonly IMediator mediator;

        public DeleteOrganizationByIdAsyncCommandHandler(
            IOrganizationRepository organizationRepository,
            IMediator mediator
        )
        {
            this.organizationRepository = organizationRepository;
            this.mediator = mediator;
        }

        public async Task<OperationResult> Handle(DeleteOrganizationByIdAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create();
            var organizationResult = await mediator.Send(new GetOrganizationByIdAsyncQuery { Id = command.Id });

            if (!organizationResult.IsSuccess)
            {
                result.AddError(organizationResult);
                return result;
            }

            try
            {
                await organizationRepository.DeleteAsync(organizationResult.Data!);
            }
            catch (Exception)
            {
                result.AddError("Organization could not be removed");
            }

            return result;
        }
    }
}
