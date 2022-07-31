using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DDDTest.Domain.Framework.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DDDTest.DataAccess.Repositories;
public class WriteRepository<T> : IRepository<T> where T : class {

    protected readonly DbContext _dbContext;
    protected readonly IHttpContextAccessor _httpContextAccessor;
    public WriteRepository(DbContext dbContext, IHttpContextAccessor httpContextAccessor = null) {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;

    }

    public virtual void Create(T item) {
        if (item == null)
            throw new ArgumentNullException("item");


        _dbContext.Set<T>().Add(item);


        this.SaveChanges();

    }

    public virtual async Task<T> CreateAsync(T item, CancellationToken cancellationToken = default) {
        if (item == null)
            throw new ArgumentNullException("item");

        await _dbContext.Set<T>().AddAsync(item, cancellationToken);


        await this.SaveChangesAsync();


        return item;

    }



    public virtual void Update(T item) {
        if (item == null)
            throw new ArgumentNullException("item");

        _dbContext.Entry(item).State = EntityState.Modified;

        this.SaveChanges(); ;

    }
    public virtual async Task<T> UpdateAsync(T item) {
        if (item == null)
            throw new ArgumentNullException("item");

        _dbContext.Entry(item).State = EntityState.Modified;

        await this.SaveChangesAsync();
        return item;
    }


    public virtual void SaveChanges() {

        try {

            _dbContext.SaveChanges();

        }
        catch (Exception ex) {
            var sb = new StringBuilder();
        }

    }

    public virtual async System.Threading.Tasks.Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
        return await _dbContext.SaveChangesAsync();
    }

    public virtual void Delete(T item) {
        _dbContext.Set<T>().Attach(item);
        _dbContext.Set<T>().Remove(item);

        this.SaveChanges(); ;

    }
    public virtual async void DeleteAsync(T item, CancellationToken cancellationToken = default) {

        _dbContext.Set<T>().Attach(item);
        _dbContext.Set<T>().Remove(item);

        await this.SaveChangesAsync();

    }
    public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default) {
        return predicate == null ? await _dbContext.Set<T>().FirstOrDefaultAsync() : await _dbContext.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
    }
}

