using Entities.Models;

namespace modsen_study_lapey.Services.Interfaces;

public interface IBookService
{
    public List<Book> GetBooks();
    public Book GetBookById();
    public Book GetBookByIban();
    public bool CreateBook(Book book);
    public bool UpdateBook(Book book);
    public bool DeleteBook(int id);
}