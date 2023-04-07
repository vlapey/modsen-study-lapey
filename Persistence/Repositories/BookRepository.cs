using Entities.Models;
using Persistence.Generics;

namespace Persistence.Repositories;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
    public BookRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        
    }
}