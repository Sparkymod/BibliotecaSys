using BibliotecaSys.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaSys.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     Gets a DbSet for the specified TEntity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity for which to get the DbSet.</typeparam>
    /// <returns>The DbSet for the specified TEntity.</returns>
    public DbSet<TEntity> Set<TEntity>() where TEntity : class => _context.Set<TEntity>();

    /// <summary>
    ///     Asynchronously saves all changes made in this context to the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation, with the number of state entries written to the database.</returns>
    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}
