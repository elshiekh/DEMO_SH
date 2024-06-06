using Demo.Application.Interfaces;

namespace Demo.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IContactRepository contactRepository)
        {
            Contacts = contactRepository;
        }

        public IContactRepository Contacts { get; set; }
    }
}
