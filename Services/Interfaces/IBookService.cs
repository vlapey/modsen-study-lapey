using Entities.Models;

namespace Services.Interfaces;

public interface IBookService
{
    Task<List<Book>> GetBooks();
    
    Task<Book?> GetBookById(int id);
    
    Task<Book?> GetBookByIban(string iban);
    
    Task<Book?> CreateBook(Book book);
    
    Task<Book?> UpdateBook(Book book);
    
    Task<bool> DeleteBook(int id);
}