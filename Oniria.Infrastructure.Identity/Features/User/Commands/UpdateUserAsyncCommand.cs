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
    public class UpdateUserAsyncCommand : IRequest<OperationResult<UserResponse>>
    {
        public UpdateUserRequest Request { get; set; }
    }

    public class UpdateUserAsyncCommandHandler : IRequestHandler<UpdateUserAsyncCommand, OperationResult<UserResponse>>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public UpdateUserAsyncCommandHandler(
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IMediator mediator
        )
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public async Task<OperationResult<UserResponse>> Handle(UpdateUserAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create<UserResponse>();
            var request = command.Request;
            var userToUpdate = await userManager.FindByIdAsync(request.Id);

            if (userToUpdate == null)
            {
                result.AddError("The user to update was not found");
                return result;
            }

            if (request.UserName != userToUpdate.UserName && (await mediator.Send(new IsUserNameTakenQuery { UserName = command.Request.UserName })).Data)
            {
                result.AddError("This username is already taken");
                return result;
            }

            if (request.Email != userToUpdate.Email &&  (await mediator.Send(new IsUserEmailTakenQuery { UserEmail = command.Request.Email })).Data)
            {
                result.AddError("This email is already in use");
                return result;
            }

            userToUpdate.UserName = request.UserName;
            userToUpdate.Status = request.Status;
            userToUpdate.Email = request.Email;

            var resultUpdate = await userManager.UpdateAsync(userToUpdate);

            if (!resultUpdate.Succeeded)
            {
                result.AddError("An ocurred an error updating the user try again");
                return result;
            }

            // Update password is optional
            if (!string.IsNullOrEmpty(request.Password))
            {
                if (!(await userManager.RemovePasswordAsync(userToUpdate)).Succeeded)
                {
                    result.AddError("An ocurred an error removing the user password");
                    return result;
                }

                if (!(await userManager.AddPasswordAsync(userToUpdate, request.Password)).Succeeded)
                {
                    result.AddError("An ocurred an error adding the user password try again");
                    return result;
                }
            }

            var response = mapper.Map<UserResponse>(userToUpdate);
            response.Roles = (await mediator.Send(new GetAllUserRolesAsyncQuery { User = userToUpdate })).Data;

            result.Data = response;

            return result;
        }
    }
}

