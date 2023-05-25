using Microsoft.EntityFrameworkCore;

namespace BibliotecaSys.Infrastructure.Data;

public interface IUnitOfWork : IDisposable
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync();
}