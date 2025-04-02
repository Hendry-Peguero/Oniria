using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Dtos.User.Request;
using Oniria.Core.Dtos.User.Response;
using Oniria.Infrastructure.Identity.Entities;

namespace Oniria.Infrastructure.Identity.Features.User.Commands
{
    public class CreateUserAsyncCommand : IRequest<OperationResult<UserResponse>>
    {
        public CreateUserRequest Request { get; set; }
    }

    public class CreateUserAsyncCommandHandler : IRequestHandler<CreateUserAsyncCommand, OperationResult<UserResponse>>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public CreateUserAsyncCommandHandler(
            UserManager<ApplicationUser> userManager,
            IMapper mapper
        )
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<OperationResult<UserResponse>> Handle(CreateUserAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create<UserResponse>();
            var request = command.Request;

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
