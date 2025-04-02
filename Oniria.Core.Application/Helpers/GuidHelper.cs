namespace Oniria.Core.Application.Helpers
{
    public static class GuidHelper
    {
        public static string Generate()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
