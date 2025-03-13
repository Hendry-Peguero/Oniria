namespace Oniria.Core.Application.Interfaces.Repositories.Maintenance
{
    public interface DeleteAsync<TypeId>
    {
        abstract Task DeleteAsync(TypeId id);
    }
}
