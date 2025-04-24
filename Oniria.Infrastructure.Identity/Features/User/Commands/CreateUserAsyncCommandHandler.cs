using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.User.Commands;
using Oniria.Core.Dtos.User.Response;
using Oniria.Infrastructure.Identity.Entities;
using Oniria.Infrastructure.Identity.Features.User.Queries;

namespace Oniria.Infrastructure.Identity.Features.User.Commands
{
    public class CreateUserAsyncCommandHandler : IRequestHandler<CreateUserAsyncCommand, OperationResult<UserResponse>>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CreateUserAsyncCommandHandler(
            UserManager<ApplicationUser> userManager,
            IMediator mediator,
            IMapper mapper
        )
        {
            this.userManager = userManager;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<OperationResult<UserResponse>> Handle(CreateUserAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult<UserResponse>.Create();
            var request = command.Request;

            if ((await mediator.Send(new IsUserNameTakenQuery { UserName = command.Request.UserName })).Data)
            {
                result.AddError("This username is already taken");
                return result;
            }

            if ((await mediator.Send(new IsUserEmailTakenQuery { UserEmail = command.Request.Email })).Data)
            {
                result.AddError("This email is already in use");
                return result;
            }

            var userToCreate = mapper.Map<ApplicationUser>(request);
            var creationResult = await userManager.CreateAsync(userToCreate, request.Password);

            if (!creationResult.Succeeded)
            {
                result.AddError("An unexpected error occurred creating the user");
                return result;
            }

            // Asignar el Id del usuario creado a la respuesta
            result.Data = mapper.Map<UserResponse>(userToCreate);

            return result;
        }
    }
}
