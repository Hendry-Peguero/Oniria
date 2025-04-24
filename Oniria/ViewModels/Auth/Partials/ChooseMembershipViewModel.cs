using Oniria.Core.Domain.Entities;

namespace Oniria.ViewModels.Auth.Partials
{
    public class ChooseMembershipViewModel
    {
        public string InputKey { get; set; }
        public List<MembershipEntity>? Memberships { get; set; }
    }
}
