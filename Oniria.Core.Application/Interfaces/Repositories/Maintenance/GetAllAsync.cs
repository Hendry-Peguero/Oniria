namespace Oniria.Core.Application.Interfaces.Repositories.Maintenance
{
    public interface GetAllAsync<Entity>
    {
        abstract Task<List<Entity>> GetAllAsync();
    }
}
