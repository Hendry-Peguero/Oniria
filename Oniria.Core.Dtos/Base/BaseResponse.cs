namespace Oniria.Core.Dtos.Base
{
    public class BaseResponse
    {
        public bool HasError { get; set; }
        public string? ErrorDescription { get; set; }
    }
}
