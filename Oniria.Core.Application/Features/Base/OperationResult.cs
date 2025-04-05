using System.Collections;

namespace Oniria.Core.Application.Features.Base
{
    public interface IOperationResult<T> where T : class
    {
        static abstract T Create();
    }

    public abstract class BaseOperationResult<TSelf> where TSelf : BaseOperationResult<TSelf>
    {
        public bool IsSuccess { get; set; } = true;
        public List<string> Messages { get; set; } = new();

        // Add error to the result
        public void AddMessage(string errorMessage)
        {
            Messages.Add(errorMessage);
        }

        public void AddError(string errorMessage)
        {
            Messages.Add(errorMessage);
            Error();
        }

        // Unsuccess UseCase
        private void Error()
        {
            if (IsSuccess) IsSuccess = false;
        }
    }

    public class OperationResult : BaseOperationResult<OperationResult>, IOperationResult<OperationResult>
    {
        private OperationResult() { }

        public static OperationResult Create()
        {
            return new OperationResult();
        }
    }

    public class OperationResult<T> : BaseOperationResult<OperationResult<T>>, IOperationResult<OperationResult<T>>
    {
        public T? Data { get; set; }

        private OperationResult() { }

        public static OperationResult<T> Create()
        {
            var result = new OperationResult<T>();

            bool isCollection =
                typeof(IEnumerable).IsAssignableFrom(typeof(T)) &&
                typeof(T) != typeof(string) &&
                !typeof(T).IsPrimitive;

            if (isCollection)
            {
                try
                {
                    result.Data = Activator.CreateInstance<T>();
                }
                catch
                {
                    result.Data = default;
                }
            }

            return result;
        }
    }
}
