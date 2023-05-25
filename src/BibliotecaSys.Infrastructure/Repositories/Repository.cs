using System.Linq.Expressions;
using System.Threading;
using BibliotecaSys.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaSys.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly IUnitOfWork _unitOfWork;

    public Repository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    ///     Asynchronously retrieves all entities of type T from the data store.
    /// </summary>
    /// <returns>A task that represents the asynchronous retrieval operation, with an enumerable collection of entities of type T.</returns>
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) => await _unitOfWork.Set<T>().ToListAsync(cancellationToken);

    /// <summary>
    ///     Asynchronously retrieves an entity of type T with the specified id from the data store.
    /// </summary>
    /// <param name="id">The id of the entity to retrieve.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task that represents the asynchronous retrieval operation, with the retrieved entity of type T or null if not found.</returns>
    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default) => await _unitOfWork.Set<T>().FindAsync(id, cancellationToken);

    /// <summary>
    ///     Asynchronously creates and persists a new entity of type T in the data store.
    /// </summary>
    /// <param name="entity">The entity to create and persist.</param>
    /// <returns>A task that represents the asynchronous creation operation, with the created entity of type T.</returns>
    public async Task<T> CreateAsync(T entity)
    {
        _unitOfWork.Set<T>().Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    ///     Asynchronously updates and persists an existing entity of type T in the data store.
    /// </summary>
    /// <param name="entity">The entity to update and persist.</param>
    /// <returns>A task that represents the asynchronous update operation, with the updated entity of type T.</returns>
    public async Task<T> UpdateAsync(T entity)
    {
        _unitOfWork.Set<T>().Update(entity);
        await _unitOfWork.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    ///     Asynchronously deletes an entity of type T with the specified id from the data store.
    /// </summary>
    /// <param name="id">The id of the entity to delete.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return;
        }
        _unitOfWork.Set<T>().Remove(entity);
        await _unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    ///     Asynchronously retrieves all entities of type T that satisfy the specified predicate from the data store.
    /// </summary>
    /// <param name="predicate">The predicate to filter the entities.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    ///     A task that represents the asynchronous retrieval operation,
    ///     with an enumerable collection of entities of type T that satisfy the predicate. </returns>
    public async Task<IEnumerable<T>> GetByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        => await _unitOfWork.Set<T>().Where(predicate).ToListAsync(cancellationToken);


    /// <summary>
    ///     Asynchronously checks whether any entities of type T satisfy the specified predicate in the data store,
    ///     and cancels the operation if the token is cancelled.
    /// </summary>
    /// <param name="predicate">The predicate to filter the entities.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation,
    ///     returning true if any entities satisfy the predicate, and false otherwise.
    /// </returns>
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        => await _unitOfWork.Set<T>().AnyAsync(predicate, cancellationToken);

    /// <summary>
    ///     Asynchronously gets the total count of entities of type T in the data store.
    /// </summary>
    /// <returns>
    ///     A task that represents the asynchronous operation,
    ///     with the total count of entities of type T.</returns>
    public async Task<int> CountAsync(CancellationToken cancellationToken) => await _unitOfWork.Set<T>().CountAsync(cancellationToken);

    /// <summary>
    ///     Asynchronously gets the count of entities of type T that satisfy the specified predicate in the data store.
    /// </summary>
    /// <param name="predicate">The predicate to filter the entities.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation, with the count of
    ///     entities of type T that satisfy the predicate.</returns>
    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        => await _unitOfWork.Set<T>().CountAsync(predicate, cancellationToken);

    /// <summary>
    ///     Asynchronously retrieves a paged collection of entities of type T from the data store.
    /// </summary>
    /// <param name="pageIndex">The index of the page to retrieve.</param>
    /// <param name="pageSize">The size of the page to retrieve.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    ///     A task that represents the asynchronous retrieval operation,
    ///     with an enumerable collection of paged entities of type T.</returns>
    public async Task<IEnumerable<T>> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        => await _unitOfWork.Set<T>().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

    /// <summary>
    ///     Asynchronously retrieves a paged collection of entities of type T that satisfy the specified predicate from the data store.
    /// </summary>
    /// <param name="pageIndex">The index of the page to retrieve.</param>
    /// <param name="pageSize">The size of the page to retrieve.</param>
    /// <param name="predicate">The predicate to filter the entities.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    ///     A task that represents the asynchronous retrieval operation,
    ///     ith an enumerable collection of paged entities of type T
    ///     that satisfy the predicate.</returns>
    public async Task<IEnumerable<T>> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        => await _unitOfWork.Set<T>().Where(predicate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

    /// <summary>
    ///     Provides a queryable interface for the entities of type T in the data store.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>
    ///     An IQueryable interface that can be used to compose and execute
    ///     queries against the entities.</returns>
    public IQueryable<T> AsQueryable(CancellationToken cancellationToken = default) => _unitOfWork.Set<T>().AsQueryable();
}