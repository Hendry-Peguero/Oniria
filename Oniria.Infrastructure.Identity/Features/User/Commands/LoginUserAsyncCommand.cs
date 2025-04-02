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
            var result = OperationResult.Create<UserResponse>();
            var request = command.Request;

            // Email or username exists
            var user = await userManager.FindByEmailAsync(request.Identifier)
                                ?? await userManager.FindByNameAsync(request.Identifier);

            if (user == null)
            {
                result.AddError($"Credentials are incorrect");
                return result;
            }

            // Validate password
            var signInResult = await signInManager.PasswordSignInAsync(
                user.UserName,
                request.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            if (!signInResult.Succeeded)
            {
                result.AddError($"Credentials are incorrect");
                return result;
            }

            // Validate Email
            if (!user.EmailConfirmed)
            {
                result.AddError($"This account is not confirmed");
                return result;
            }

            // Validate Status
            if (user.Status == StatusEntity.INACTIVE)
            {
                result.AddError($"This account is not active, please contact the administrator");
                return result;
            }

            var userResponse = mapper.Map<UserResponse>(user);
            userResponse.Roles = (await mediator.Send(new GetAllUserRolesAsyncQuery { User = user })).Data;
            result.Data = userResponse;

            return result;
        }
    }
}