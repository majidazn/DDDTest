using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DDDTest.DataAccess.Infrastrutures;
using DDDTest.Domain.Framework.Repositories;
using EventStore.Client;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DDDTest.DataAccess.Repositories;
public class Repository<T> : IRepository<T> where T : class {

    protected readonly DbContext _dbContext;
    protected readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMediator _mediator;
    private readonly EventStoreClient _eventStoreClient;

    public Repository(DbContext dbContext, IMediator mediator, EventStoreClient eventStoreClient, IHttpContextAccessor httpContextAccessor = null) {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _eventStoreClient = eventStoreClient;

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


       // await this.SaveChangesAsync();


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
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        await _mediator.DispatchDomainEventsAsync(_dbContext, _eventStoreClient);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result;
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

