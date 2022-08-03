using DDDTest.DataAccess.Configurations;
using DDDTest.Domain.Aggregates.UserAggregate.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DDDTest.DataAccess.Context;
public class UserBoundedContextCommand : DbContext {
    //Add-Migration NewMigration -Project DDDTest.DataAccess -Context UserBoundedContextCommand
    //update-database -Context UserBoundedContextCommand
    public UserBoundedContextCommand(DbContextOptions<UserBoundedContextCommand> options)
        : base(options) {
    }

    #region DbSets
    public DbSet<User> Users { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
         modelBuilder.ApplyConfiguration(new UserConfiguration());
    }

}

