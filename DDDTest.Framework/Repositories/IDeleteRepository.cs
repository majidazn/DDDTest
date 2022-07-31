using System.Linq.Expressions;

namespace DDDTest.Domain.Framework.Repositories;
public interface IDeleteRepository<T> where T : class {

    event EventHandler<EntityDeletingEventArgs<T>> BeforeDeletingRecord;

    event EventHandler<EntityDeletingEventArgs<T>> DeletingRecord;

    event EventHandler<EntityDeletingEventArgs<T>> RecordDeleted;


    bool DeleteUoW(T item);
    void Delete(T item);
    void DeleteAsync(T item, CancellationToken cancellationToken = default);
    void Delete(IEnumerable<T> entities);
    void DeleteAsync(IEnumerable<T> entities);
    Task<int> DeleteAsyncUoW(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    void Delete(Expression<Func<T, bool>> predicate);
    Task<int> DeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    void RemoveEntitiesUoW<S>(IEnumerable<S> entities) where S : class;
    int DeleteUoW(Expression<Func<T, bool>> predicate);
}

