using System.Linq.Expressions;

namespace DDDTest.Domain.Framework.Repositories;
public interface ISaveRepository<T> where T : class {

    event EventHandler<EntitySavingEventArgs<T>> BeforeSavingRecord;
    event EventHandler<EntitySavingEventArgs<T>> SavingRecord;
    event EventHandler<EntitySavingEventArgs<T>> RecordSaved;
    event EventHandler<EntitySavingEventArgs<T>> UpdatingRecord;
    event EventHandler<EntitySavingEventArgs<T>> RecordUpdated;

    void Create(T item);

    Task<T> CreateAsync(T item, CancellationToken cancellationToken = default);

    Task<T> CreateAsyncUoW(T item, CancellationToken cancellationToken = default);

    Task<bool> CreateMultiAsyncUoW(IEnumerable<T> items, CancellationToken cancellationToken = default);

    void Update(T item);

    Task<T> UpdateAsync(T item);

    T UpdateUoW(T item);
    void UpdateWithAttachUoW(T item);

    void CreateMulti(IEnumerable<T> items);

    Task<bool> CreateMultiAsync(IEnumerable<T> items, CancellationToken cancellationToken = default);

    int BulkUpdate(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatePredicate);

    Task<int> BulkUpdateAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatePredicate, CancellationToken cancellationToken = default);

    void UpdateWithAttach(T item);

    void UpdateWithAttachAsync(T item);
}

