using modsen_study_lapey.Data;
using modsen_study_lapey.Models;
using modsen_study_lapey.Services.Interfaces;

namespace modsen_study_lapey.Services;

public class BookService : IBookService
{
    private static AppDbContext _appDbContext;
    
    public BookService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public List<Book> GetBooks()
    {
        throw new NotImplementedException();
    }

    public Book GetBookById()
    {
        throw new NotImplementedException();
    }

    public Book GetBookByIBAN()
    {
        throw new NotImplementedException();
    }

    public bool CreateBook(Book book)
    {
        throw new NotImplementedException();
    }

    public bool EditBook(Book book)
    {
        throw new NotImplementedException();
    }

    public bool DeleteBook(int id)
    {
        throw new NotImplementedException();
    }
}