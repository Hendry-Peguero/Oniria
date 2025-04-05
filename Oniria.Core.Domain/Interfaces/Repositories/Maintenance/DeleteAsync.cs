namespace Oniria.Core.Domain.Interfaces.Repositories.Maintenance
{
    public interface DeleteAsync<Entity>
    {
        abstract Task DeleteAsync(Entity entity);
    }
}
