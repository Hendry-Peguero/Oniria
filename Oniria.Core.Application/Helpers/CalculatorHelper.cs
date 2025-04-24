using Oniria.Core.Domain.Constants;

namespace Oniria.Core.Application.Helpers
{
    public static class CalculatorHelper
    {
        public static int? GetAmountTokenByMembership(string membershipId)
        {
            return membershipId switch
            {
                MembershipIdsConstants.PatientBasic => 1,
                MembershipIdsConstants.PatientStandard => 10,
                MembershipIdsConstants.PatientPremium => null,
                _ => 0
            };
        }
    }
}
