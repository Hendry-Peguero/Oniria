using MediatR;
using Microsoft.AspNetCore.Mvc.Razor;
using Oniria.Core.Application.Features.User.Queries;
using Oniria.Core.Dtos.User.Response;

namespace Oniria.Extensions
{
    public abstract class BaseRazor : RazorPage<dynamic>
    {
        public async Task<UserResponse?> LoggedUserAsync()
        {
            var Mediator = Context.RequestServices.GetService<IMediator>();
            return (await Mediator!.Send(new GetUserSessionAsyncQuery())).Data;
        }
    }
}
