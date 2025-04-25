using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Dtos.User.Request;
using Oniria.Core.Dtos.User.Response;
using Oniria.Infrastructure.Identity.Entities;
using Oniria.Infrastructure.Identity.Features.User.Queries;

namespace Oniria.Infrastructure.Identity.Features.User.Commands
{
    public class LoginUserWithoutSessionAsyncCommand : IRequest<OperationResult<UserResponse>>
    {
        public AuthenticationRequest Request { get; set; }
    }

    public class LoginUserWithoutSessionAsyncCommandHandler : IRequestHandler<LoginUserWithoutSessionAsyncCommand, OperationResult<UserResponse>>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public LoginUserWithoutSessionAsyncCommandHandler(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            IMediator mediator
        )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public async Task<OperationResult<UserResponse>> Handle(LoginUserWithoutSessionAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult<UserResponse>.Create();
            var request = command.Request;

            var user = await userManager.FindByEmailAsync(request.Identifier) ??
                       await userManager.FindByNameAsync(request.Identifier);

            var signInResult = await signInManager.PasswordSignInAsync(
                user?.UserName ?? "",
                request.Password,
                isPersistent: false,
                lockoutOnFailure: false
            );

            if (user == null || !signInResult.Succeeded)
            {
                result.AddError("Credentials are incorrect");
                return result;
            }

            if (!user.EmailConfirmed)
            {
                result.AddError("This account is not confirmed");
                return result;
            }

            if (user.Status == Core.Domain.Enums.StatusEntity.INACTIVE)
            {
                result.AddError("This account is not active, please contact the administrator");
                return result;
            }

            var userResponse = mapper.Map<UserResponse>(user);
            userResponse.Roles = (await mediator.Send(new GetAllUserRolesAsyncQuery { User = user })).Data;

            result.Data = userResponse;
            return result;
        }
    }
}
