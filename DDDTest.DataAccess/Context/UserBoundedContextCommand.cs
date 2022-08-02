using DDDTest.Domain.Aggregates.UserAggregate.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DDDTest.DataAccess.Context;
public class UserBoundedContextCommand : DbContext {
    public UserBoundedContextCommand(DbContextOptions<UserBoundedContextCommand> options)
        : base(options) {
    }

    #region DbSets
    public DbSet<User> Users { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        // modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
    //public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken)) {
    //    // Dispatch Domain Events collection. 
    //    // Choices:
    //    // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
    //    // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
    //    // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
    //    // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
    //    await _mediator.DispatchDomainEventsAsync(this);

    //    // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
    //    // performed through the DbContext will be committed
    //    var result = await base.SaveChangesAsync(cancellationToken);

    //    return true;
    //}
}

