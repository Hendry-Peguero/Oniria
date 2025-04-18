using MediatR;
using Oniria.Core.Application.Contexts;
using System.Linq.Expressions;

namespace Oniria.Core.Application.Features.Base
{
    public class MediatorWrapper : IMediatorWrapper
    {
        private readonly ISender sender;
        private readonly IUserIncludeContext userIncludeContext;

        public MediatorWrapper(ISender sender, IUserIncludeContext userIncludeContext)
        {
            this.sender = sender;
            this.userIncludeContext = userIncludeContext;
        }

        public Task<OperationResult<TEntity>> Send<TEntity>(
            IRequest<OperationResult<TEntity>> request,
            params Expression<Func<TEntity, object>>[] includes
        )
        {
            userIncludeContext.AddIncludes(includes);
            return sender.Send(request);
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            return sender.Send(request);
        }
    }

    public interface IMediatorWrapper
    {
        Task<OperationResult<TEntity>> Send<TEntity>(
            IRequest<OperationResult<TEntity>> request,
            params Expression<Func<TEntity, object>>[] includes
        );

        Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
    }
}
