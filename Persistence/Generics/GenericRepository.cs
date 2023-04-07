using System.Linq.Expressions;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces.Persistence;

namespace Persistence.Generics;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
{
    private readonly AppDbContext _appDbContext;

    protected GenericRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<List<T>> GetItems(Expression<Func<T, bool>>? filter = null)
    {
        if (filter is null)
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }
        
        var items = await _appDbContext.Set<T>()
            .Where(filter)
            .ToListAsync();
        
        return items;
    }
    
    public async Task<T?> GetItemById(int id)
    {
        var item = await _appDbContext.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
        
        return item;
    }
    
    public async Task<T?> GetItemByFilter(Expression<Func<T, bool>> filter)
    {
        var item = await _appDbContext.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(filter);
        
        return item;
    }

    public async Task<T?> CreateItem(T item)
    {
        await _appDbContext.Set<T>().AddAsync(item);
        await _appDbContext.SaveChangesAsync();
        
        var createdEntity = await GetItemById(item.Id);
        
        return createdEntity;
    }

    public async Task<T?> UpdateItem(T item)
    {
        var entity =  await GetItemById(item.Id);
        
        if (entity is null)
        {
            return null;
        }
        
        _appDbContext.Set<T>().Update(item);
        await _appDbContext.SaveChangesAsync();
        
        return item;
    }

    public async Task<bool> DeleteItem(int id)
    {
        var entity = await _appDbContext.Set<T>()
            .FirstOrDefaultAsync(item => item.Id == id);
        
        if (entity is null)
        {
            return false;
        }
        
        _appDbContext.Set<T>().Remove(entity);
        await _appDbContext.SaveChangesAsync();
        
        return true;
    }
}