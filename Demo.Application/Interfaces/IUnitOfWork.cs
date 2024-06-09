namespace Demo.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IContactRepository Contacts { get; }
        IBookRepository Books { get; }
    }
}
