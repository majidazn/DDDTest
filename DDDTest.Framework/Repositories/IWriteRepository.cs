namespace DDDTest.Domain.Framework.Repositories;
public interface IWriteRepository<T> /*: ISaveRepository<T>, IDeleteRepository<T>*/ where T : class {

    void Create(T item);
    Task<T> CreateAsync(T item, CancellationToken cancellationToken = default);

    void Update(T item);

    Task<T> UpdateAsync(T item);

    void Delete(T item);
    void DeleteAsync(T item, CancellationToken cancellationToken = default);

    System.Threading.Tasks.Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    void SaveChanges();
}

