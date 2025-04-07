using Microsoft.AspNetCore.Identity;
using Oniria.Core.Domain.Enums;
using Oniria.Core.Dto.User.Interfaces;

namespace Oniria.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
        public StatusEntity Status { get; set; }
    }
}
