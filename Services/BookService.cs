using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services.Interfaces;

namespace Services;

public class BookService : IBookService
{
    private readonly AppDbContext _appDbContext;
    
    public BookService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<List<Book>> GetBooks()
    {
        var books = await _appDbContext.Books.ToListAsync();
        return books;
    }

    public Task<Book?> GetBookById(int id)
    {
        var book = _appDbContext.Books.FirstOrDefaultAsync(book => book.Id == id);
        return book;
    }

    public async Task<Book?> GetBookByIban(string iban)
    {
        var book = await _appDbContext.Books.FirstOrDefaultAsync(book => book.Iban == iban);
        return book;
    }

    public async Task<Book?> CreateBook(Book book)
    {
        await _appDbContext.Books.AddAsync(book);
        await _appDbContext.SaveChangesAsync();
        var createdEntity = await _appDbContext.Books.FirstOrDefaultAsync(bookEntity => bookEntity.Id == book.Id);
        return createdEntity;
    }

    public async Task<Book?> UpdateBook(Book book)
    {
        _appDbContext.Books.Update(book);
        await _appDbContext.SaveChangesAsync();
        var updatedEntity = await _appDbContext.Books.FirstOrDefaultAsync(bookEntity => bookEntity.Id == book.Id);
        return updatedEntity;
    }

    public async Task<bool> DeleteBook(int id)
    {
        var entity = await _appDbContext.Books.FirstOrDefaultAsync(bookEntity => bookEntity.Id == id);
        
        if (entity is null)
        {
            return false;
        }
        
        _appDbContext.Books.Remove(entity);
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}