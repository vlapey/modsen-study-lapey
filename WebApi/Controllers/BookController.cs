using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using WebApi.Dto;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BookController : ControllerBase
{

    private readonly IBookService _bookService;
    private readonly IMapper _mapper;
    
    public BookController(IBookService bookService, IMapper mapper)
    {
        _bookService = bookService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _bookService.GetBooks();
        
        var bookDto = _mapper.Map<List<BookDto>>(books);
        
        return Ok(bookDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await _bookService.GetBookById(id);
        
        if (book is null)
        {
            return NotFound();
        }
        
        var bookDto = _mapper.Map<BookDto>(book);
        
        return Ok(bookDto);
    }

    [HttpGet("{iban}")]
    public async Task<IActionResult> GetBookByIban(string iban)
    {
        var book = await _bookService.GetBookByIban(iban);
        
        if (book is null)
        {
            return NotFound();
        }
        
        var bookDto = _mapper.Map<BookDto>(book);
        
        return Ok(bookDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookDto bookDto)
    {
        var book = _mapper.Map<Book>(bookDto);
        
        var createdBook = await _bookService.CreateBook(book);
        
        if (createdBook is null)
        {
            return BadRequest();
        }
        
        var createdBookDto = _mapper.Map<BookDto>(createdBook);
        
        return Ok(createdBookDto);
    }
    
    [HttpPatch]
    public async Task<IActionResult> UpdateBook(BookDto bookDto)
    {
        var book = _mapper.Map<Book>(bookDto);
        
        var updatedBook = await _bookService.UpdateBook(book);
        
        if (updatedBook is null)
        {
            return BadRequest();
        }
        
        var updatedBookDto = _mapper.Map<BookDto>(updatedBook);
        
        return Ok(updatedBookDto);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var res = await _bookService.DeleteBook(id);
        
        if (res == false)
        {
            return BadRequest();
        }

        return Ok();
    }
}