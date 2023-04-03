using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modsen_study_lapey.Data;
using modsen_study_lapey.Services.Interfaces;

namespace modsen_study_lapey.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{

    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        throw new NotImplementedException();
    }
}