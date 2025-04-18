using Microsoft.AspNetCore.Mvc.Filters;
using Oniria.Helpers;
using Oniria.Services;

namespace Oniria.Attributes
{
    public enum GoHomeWhen
    {
        USER_IN_SESSION,
        USER_OUT_SESSION
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class GoHomeAttribute : Attribute, IAsyncActionFilter
    {
        private readonly GoHomeWhen state;

        public GoHomeAttribute(GoHomeWhen state)
        {
            this.state = state;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userContext = context.HttpContext.RequestServices.GetRequiredService<IUserContextService>();
            var userLogged = await userContext.GetLoggedUser();

            var shouldRedirect = state switch
            {
                GoHomeWhen.USER_IN_SESSION => userLogged != null,
                GoHomeWhen.USER_OUT_SESSION => userLogged == null,
                _ => false
            };

            if (shouldRedirect)
            {
                context.Result = Redirections.GetHomeByUserRole(userLogged?.Roles?.FirstOrDefault());
            }
            else
            {
                await next();
            }
        }
    }

}
