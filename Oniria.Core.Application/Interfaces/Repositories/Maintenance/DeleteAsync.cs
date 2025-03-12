namespace Oniria.Core.Application.Interfaces.Repositories.Maintenance
{
    public interface DeleteAsync<TypeId>
    {
        abstract Task UpdateAsync(TypeId id);
    }
}
