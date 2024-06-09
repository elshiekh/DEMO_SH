using Demo.Application.Interfaces;

namespace Demo.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IContactRepository contactRepository, IBookRepository bookRepository)
        {
            Contacts = contactRepository;
            Books = bookRepository;
        }

        public IContactRepository Contacts { get; set; }
        public IBookRepository Books { get; set; }
    }
}
