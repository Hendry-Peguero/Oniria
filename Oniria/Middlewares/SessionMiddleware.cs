using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Oniria.Helpers;
using Oniria.Infrastructure.Identity.Features.User.Queries;

namespace Oniria.Middlewares
{
    public class SessionMiddleware : IAsyncActionFilter
    {
        private readonly IMediator mediator;

        public SessionMiddleware(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if ((await mediator.Send(new IsUserInSessionAsyncQuery())).Data)
            {
                context.Result = Redirections.GetHomeByUserRole(
                    (await mediator.Send(new GetUserSessionAsyncQuery())).Data?.Roles?.FirstOrDefault()
                );
            }
            else
            {
                await next();
            }
        }
    }
}
