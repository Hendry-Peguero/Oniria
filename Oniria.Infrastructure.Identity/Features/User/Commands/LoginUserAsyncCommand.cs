using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Enums;
using Oniria.Core.Dtos.User.Request;
using Oniria.Core.Dtos.User.Response;
using Oniria.Infrastructure.Identity.Entities;
using Oniria.Infrastructure.Identity.Features.User.Queries;

namespace Oniria.Infrastructure.Identity.Features.User.Commands
{
    public class LoginUserAsyncCommand : IRequest<OperationResult<UserResponse>>
    {
        public AuthenticationRequest Request { get; set; }
    }

    public class LoginUserAsyncCommandHandler : IRequestHandler<LoginUserAsyncCommand, OperationResult<UserResponse>>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public LoginUserAsyncCommandHandler(
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

        public async Task<OperationResult<UserResponse>> Handle(LoginUserAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult<UserResponse>.Create();
            var request = command.Request;

            // Email or username exists
            var userByNameOrEmail = 
                await userManager.FindByEmailAsync(request.Identifier) ?? 
                await userManager.FindByNameAsync(request.Identifier);

            // Try Sing In
            var signInResult = await signInManager.PasswordSignInAsync(
                userByNameOrEmail?.UserName ?? "",
                request.Password,
                isPersistent: false,
                lockoutOnFailure: false
            );

            if (userByNameOrEmail == null  || !signInResult.Succeeded)
            {
                result.AddError($"Credentials are incorrect");
                return result;
            }

            // Validate Email
            if (!userByNameOrEmail.EmailConfirmed)
            {
                result.AddError($"This account is not confirmed");
                return result;
            }

            // Validate Status
            if (userByNameOrEmail.Status == StatusEntity.INACTIVE)
            {
                result.AddError($"This account is not active, please contact the administrator");
                return result;
            }

            // Fill response
            var userResponse = mapper.Map<UserResponse>(userByNameOrEmail);
            userResponse.Roles = (await mediator.Send(new GetAllUserRolesAsyncQuery { User = userByNameOrEmail })).Data;
            result.Data = userResponse;

            // Set User Session
            var sessionResult = await mediator.Send(new SetUserSessionAsyncCommand { User = userResponse });

            if (!sessionResult.IsSuccess)
            {
                result.AddError($"A server error occurred, please contact the administrator");
            }

            return result;
        }
    }
}