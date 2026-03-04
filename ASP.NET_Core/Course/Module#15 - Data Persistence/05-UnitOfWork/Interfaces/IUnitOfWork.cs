
namespace UnitOfWork.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository ProductRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}