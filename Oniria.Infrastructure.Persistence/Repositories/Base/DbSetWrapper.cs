using Microsoft.EntityFrameworkCore;
using Oniria.Core.Application.Contexts;
using Oniria.Infrastructure.Persistence.Contexts;
using System.Linq.Expressions;

namespace Oniria.Infrastructure.Persistence.Repositories.Base
{
    public class DbSetWrapper<TEntity> where TEntity : class
    {
        public readonly ApplicationContext context;
        public readonly IUserIncludeContext userIncludeContext;

        public DbSetWrapper(
            ApplicationContext context, 
            IUserIncludeContext userIncludeContext
        )
        {
            this.context = context;
            this.userIncludeContext = userIncludeContext;
        }


        public IQueryable<TEntity> Query()
        {
            var query = context.Set<TEntity>().AsQueryable();

            var includeType = userIncludeContext.GetLatestIncludeType();

            if (includeType == IncludeType.Lambda)
            {
                var lambdaIncludes = userIncludeContext.GetLatestLambdaIncludes()
                    .OfType<Expression<Func<TEntity, object>>>();

                foreach (var include in lambdaIncludes) query = query.Include(include);
            }
            else if (includeType == IncludeType.String)
            {
                var stringIncludes = userIncludeContext.GetLatestStringIncludes();

                foreach (var include in stringIncludes) query = query.Include(include);
            }

            userIncludeContext.RemoveLatestIncludeByType();

            return query;
        }
    }
}
