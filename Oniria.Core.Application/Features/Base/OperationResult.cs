using System.Collections;

namespace Oniria.Core.Application.Features.Base
{
    public interface IBaseOperationResult
    {
        bool IsSuccess { get; }
        List<string> Messages { get; }
    }

    public abstract class BaseOperationResult : IBaseOperationResult
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

        public void AddError(List<string> errorMessages)
        {
            foreach (var message in errorMessages) {
                AddError(message);
            }
        }

        public void AddError(IBaseOperationResult operationResult)
        {
            AddError(operationResult.Messages);
        }


        // Unsuccess UseCase
        private void Error()
        {
            if (IsSuccess) IsSuccess = false;
        }
    }

    public interface IOperationResult<T> where T : class
    {
        static abstract T Create();
    }


    public class OperationResult : BaseOperationResult, IOperationResult<OperationResult>
    {
        private OperationResult() { }

        public static OperationResult Create()
        {
            return new OperationResult();
        }
    }

    public class OperationResult<T> : BaseOperationResult, IOperationResult<OperationResult<T>>
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
