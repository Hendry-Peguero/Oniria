namespace Oniria.Core.Domain.Interfaces.Repositories.Maintenance
{
    public interface GetAllAsync<Entity>
    {
        abstract Task<List<Entity>> GetAllAsync();
    }
}
