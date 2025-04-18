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

        // For one Entity
        public Task<OperationResult<TEntity>> Send<TEntity>(
            IRequest<OperationResult<TEntity>> request,
            params Expression<Func<TEntity, object>>[] includes
        )
        {
            userIncludeContext.AddIncludes(includes);
            return sender.Send(request);
        }

        // For List of Entities
        public Task<OperationResult<List<TEntity>>> Send<TEntity>(
            IRequest<OperationResult<List<TEntity>>> request,
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

        public Task<OperationResult<List<TEntity>>> Send<TEntity>(
            IRequest<OperationResult<List<TEntity>>> request,
            params Expression<Func<TEntity, object>>[] includes
        );

        Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
    }
}
