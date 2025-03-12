namespace Oniria.Core.Application.Features.Base
{
    public abstract class BaseUseCaseResult {
        public bool IsSuccess { get; set; } = true;
        public List<string> Errors { get; set; } = new();


        // Add error to the result
        public BaseUseCaseResult AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
            Error();
            return this;
        }
        public BaseUseCaseResult AddError(string[] errorsMessage)
        {
            foreach (var error in errorsMessage)
            {
                Errors.Add(error);
            }
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
