namespace Oniria.Core.Domain.Interfaces.Repositories.Maintenance
{
    public interface DeleteAsync<TypeId>
    {
        abstract Task DeleteAsync(TypeId id);
    }
}
