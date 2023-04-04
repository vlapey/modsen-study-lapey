using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modsen_study_lapey.Data;
using modsen_study_lapey.Models;

namespace modsen_study_lapey.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BookController : ControllerBase
{

    private readonly AppDbContext _context;

    public BookController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _context.Books.ToListAsync();
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        return Ok(book);
    }

    [HttpGet("{Iban}")]
    public async Task<IActionResult> GetBookByIban(string Iban)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Iban == Iban);
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetAllBooks", book.Id, book);
    }

    [HttpPatch]
    public async Task<IActionResult> Patch(int id, Book book)
    {
        var newBook = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        
        book.Id = id;
        
        if (newBook is null)
        {
            return BadRequest("Invalid id");
        }

        if (!String.IsNullOrEmpty(book.Iban))
        {
            newBook.Iban = book.Iban;
        }
        
        if (!String.IsNullOrEmpty(book.Name))
        {
            newBook.Name = book.Name;
        }
        
        if (!String.IsNullOrEmpty(book.Genre))
        {
            newBook.Genre = book.Genre;
        }
        
        if (!String.IsNullOrEmpty(book.Description))
        {
            newBook.Description = book.Description;
        }
        
        if (!String.IsNullOrEmpty(book.Author))
        {
            newBook.Author = book.Author;
        }

        newBook.BookTaken = book.BookTaken;
        newBook.BookWillBeReturned = book.BookWillBeReturned;
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        if (book is null)
        {
            return BadRequest("Invalid id");
        }
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}