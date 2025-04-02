using Oniria.Core.Domain.Enums;

namespace Oniria.Core.Dtos.User.Response
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<ActorsRoles>? Roles { get; set; }
        public StatusEntity Status { get; set; }
    }
}
