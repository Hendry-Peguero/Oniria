namespace Oniria.Core.Application.Features.Base
{
    public abstract class BaseUseCaseResult {
        public bool IsSuccess { get; set; } = true;
        public List<string> Messages { get; set; } = new();


        // Add error to the result
        public BaseUseCaseResult AddMessage(string errorMessage)
        {
            Messages.Add(errorMessage);
            return this;
        }

        public BaseUseCaseResult AddError(string errorMessage)
        {
            Messages.Add(errorMessage);
            Error();
            return this;
        }


        // Unsuccess UseCase
        private void Error()
        {
            if (IsSuccess) IsSuccess = false;
        }
    }

    public class UseCaseResult<T> : BaseUseCaseResult
    {
        public T? Data { get; set; }
    }

    // Versión no genérica para resultados sin datos
    public class UseCaseResult : BaseUseCaseResult { }
}
