using Microsoft.AspNetCore.Identity;
using Oniria.Core.Domain.Enums;

namespace Oniria.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public StatusEntity Status { get; set; }
    }
}
