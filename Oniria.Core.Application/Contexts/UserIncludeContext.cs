using System.Linq.Expressions;

namespace Oniria.Core.Application.Contexts
{
    public class UserIncludeContext : IUserIncludeContext
    {
        private readonly Stack<List<LambdaExpression>> _includeStack = new();

        public void AddIncludes(params LambdaExpression[] includes)
        {
            _includeStack.Push(includes.ToList());
        }

        public List<LambdaExpression> GetLatestIncludes()  => _includeStack.Count > 0 ? _includeStack.Peek() : new List<LambdaExpression>();

        public void RemoveLatestIncludes()
        {
            if (_includeStack.Count > 0)
            {
                _includeStack.Pop();
            }
        }

        public void ClearAll() => _includeStack.Clear();
    }

    public interface IUserIncludeContext
    {
        void AddIncludes(params LambdaExpression[] includes);
        List<LambdaExpression> GetLatestIncludes();
        void RemoveLatestIncludes();
        void ClearAll();
    }
}
