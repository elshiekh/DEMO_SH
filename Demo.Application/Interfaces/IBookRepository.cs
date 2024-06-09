using Demo.Core.Entities;

namespace Demo.Application.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<PagedResults<Book>> GetBooksAsync(GetBooksQuery requestParmater);
    }
}
