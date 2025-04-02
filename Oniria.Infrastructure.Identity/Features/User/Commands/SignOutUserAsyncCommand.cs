using MediatR;
using Microsoft.AspNetCore.Identity;
using Oniria.Core.Application.Features.Base;
using Oniria.Infrastructure.Identity.Entities;

namespace Oniria.Infrastructure.Identity.Features.User.Commands
{
    public class SignOutUserAsyncCommand : IRequest<OperationResult> { }

    public class SignOutUserAsyncCommandHandler : IRequestHandler<SignOutUserAsyncCommand, OperationResult>
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public SignOutUserAsyncCommandHandler(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<OperationResult> Handle(SignOutUserAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create();

            try
            {
                await signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {
                result.AddError("An error occurred while closing session");
            }

            return result;
        }
    }
}
