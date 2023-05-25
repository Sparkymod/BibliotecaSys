using System.Linq.Expressions;

namespace BibliotecaSys.Infrastructure.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    IQueryable<T> AsQueryable(CancellationToken cancellationToken = default);
}
