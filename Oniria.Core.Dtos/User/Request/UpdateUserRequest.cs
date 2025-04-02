using Oniria.Core.Domain.Enums;

namespace Oniria.Core.Dtos.User.Request
{
    public class UpdateUserRequest
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }
        public string Email { get; set; }
        public StatusEntity Status { get; set; }
    }
}
