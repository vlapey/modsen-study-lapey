using modsen_study_lapey.Models;

namespace modsen_study_lapey.Services.Interfaces;

public interface IBookService
{
    public List<Book> GetBooks();
    public Book GetBookById();
    public Book GetBookByIBAN();
    public bool CreateBook(Book book);
    public bool EditBook(Book book);
    public bool DeleteBook(int id);
}