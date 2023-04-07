using System.Linq.Expressions;
using Entities;

namespace Services.Interfaces.Persistence;

public interface IGenericRepository<T> where T : class, IEntity
{
    Task<List<T>> GetItems(Expression<Func<T, bool>>? filter = null);
    
    Task<T?> GetItemById(int id);
    
    Task<T?> GetItemByFilter(Expression<Func<T, bool>> filter);
    
    Task<T?> CreateItem(T item);
    
    Task<T?> UpdateItem(T item);
    
    Task<bool> DeleteItem(int id);
}
