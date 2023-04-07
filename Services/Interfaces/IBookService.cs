using Entities.Models;

namespace Services.Interfaces;

public interface IBookService
{
    public Task<List<Book>> GetBooks();
    public Task<Book?> GetBookById(int id);
    public Task<Book?> GetBookByIban(string iban);
    public Task<Book?> CreateBook(Book book);
    public Task<Book?> UpdateBook(Book book);
    public Task<bool> DeleteBook(int id);
}