
using UnitOfWork.Data;
using UnitOfWork.Interfaces;

namespace UnitOfWork.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork, IDisposable
{
    public IProductRepository ProductRepository => field ??= new ProductRepository(context);

    public void Dispose()
    {
        context.Dispose();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync();
    }
}