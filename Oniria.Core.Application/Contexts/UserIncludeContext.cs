using System.Linq.Expressions;

namespace Oniria.Core.Application.Contexts
{
    public enum IncludeType
    {
        Lambda,
        String
    }

    public class UserIncludeContext : IUserIncludeContext
    {
        private readonly Stack<List<LambdaExpression>> _lambdaIncludesStack = new();
        private readonly Stack<List<string>> _stringIncludesStack = new();
        private readonly Stack<IncludeType> _includeTypeStack = new();


        public void AddIncludes(params LambdaExpression[] includes)
        {
            _lambdaIncludesStack.Push(includes.ToList());
            _includeTypeStack.Push(IncludeType.Lambda);
        }

        public void AddIncludes(params string[] includes)
        {
            _stringIncludesStack.Push(includes.ToList());
            _includeTypeStack.Push(IncludeType.String);
        }

        public List<LambdaExpression> GetLatestLambdaIncludes()
            => _lambdaIncludesStack.Count > 0 ? _lambdaIncludesStack.Peek() : new();

        public List<string> GetLatestStringIncludes()
            => _stringIncludesStack.Count > 0 ? _stringIncludesStack.Peek() : new();

        public IncludeType? GetLatestIncludeType()
            => _includeTypeStack.Count > 0 ? _includeTypeStack.Peek() : null;
        
        public void RemoveLatestIncludeByType()
        {
            if (_includeTypeStack.Count == 0) return;

            var type = _includeTypeStack.Pop();

            if (type == IncludeType.Lambda)
                _lambdaIncludesStack.Pop();
            else if (type == IncludeType.String)
                _stringIncludesStack.Pop();
        }

        public void ClearAll()
        {
            _lambdaIncludesStack.Clear();
            _stringIncludesStack.Clear();
            _includeTypeStack.Clear();
        }
    }

    public interface IUserIncludeContext
    {
        void AddIncludes(params LambdaExpression[] includes);
        void AddIncludes(params string[] includes);
        List<LambdaExpression> GetLatestLambdaIncludes();
        List<string> GetLatestStringIncludes();
        IncludeType? GetLatestIncludeType();
        void RemoveLatestIncludeByType();
        void ClearAll();
    }
}
