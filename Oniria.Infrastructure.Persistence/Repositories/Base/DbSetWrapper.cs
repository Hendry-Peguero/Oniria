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

            var includes = userIncludeContext.GetLatestIncludes()
                .OfType<Expression<Func<TEntity, object>>>();

            foreach (var include in includes) {
                query = query.Include(include);
            }

            userIncludeContext.RemoveLatestIncludes();

            return query;
        }
    }
}
