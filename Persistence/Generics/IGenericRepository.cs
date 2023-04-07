using Entities;

namespace Persistence.Generics;

public interface IGenericRepository<T> where T : class, IEntity
{
    public Task<List<T>> GetItems();
    public Task<T?> GetItemById(int id);
    public Task<T?> CreateItem(T item);
    public Task<T?> EditItem(T item);
    public Task<bool> DeleteItem(int id);
}
