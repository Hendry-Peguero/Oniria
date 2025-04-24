using Microsoft.AspNetCore.Mvc.ModelBinding;
using Oniria.Core.Application.Features.Base;

namespace Oniria.Extensions
{
    public static class ModelStateExtension
    {
        public static void AddGeneralError(this ModelStateDictionary modelState, IBaseOperationResult operationResult)
        {
            modelState.AddModelError(string.Empty, operationResult.LastMessage());
        }
    }
}
