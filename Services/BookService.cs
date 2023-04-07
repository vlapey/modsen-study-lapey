using Entities.Models;
using Services.Interfaces;
using Services.Interfaces.Persistence;

namespace Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    
    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    public async Task<List<Book>> GetBooks()
    {
        var books = await _bookRepository.GetItems();
        return books;
    }

    public Task<Book?> GetBookById(int id)
    {
        var book = _bookRepository.GetItemById(id);
        return book;
    }

    public async Task<Book?> GetBookByIban(string iban)
    {
        var book = await _bookRepository.GetItemByFilter(item => item.Iban == iban);
        return book;
    }

    public async Task<Book?> CreateBook(Book book)
    {
        var createdEntity = await _bookRepository.CreateItem(book);
        return createdEntity;
    }

    public async Task<Book?> UpdateBook(Book book)
    {
        var updatedEntity =  await _bookRepository.UpdateItem(book);
        return updatedEntity;
    }

    public async Task<bool> DeleteBook(int id)
    {
        return await _bookRepository.DeleteItem(id);
    }
}