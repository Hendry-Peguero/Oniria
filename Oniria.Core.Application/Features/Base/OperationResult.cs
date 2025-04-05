using System.Collections;

namespace Oniria.Core.Application.Features.Base
{
    public abstract class BaseOperationResult
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

    public class OperationResult : BaseOperationResult
    {
        public static OperationResult Create()
        {
            return new OperationResult();
        }

        public static OperationResult<T> Create<T>()
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

    public class OperationResult<T> : BaseOperationResult
    {
        public T? Data { get; set; }
    }
}
