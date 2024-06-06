using Demo.Core.Entities;

namespace Demo.Application.Interfaces
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<Demo.Core.Entities.PagedResults<Demo.Core.Entities.Contact>> GetContactsAsync(string searchString = "", int page = 1, int pageSize = 10);
    }
}
