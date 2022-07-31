using DDDTest.Domain.Aggregates.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDTest.DataAccess.Context {
    public class UserBoundedContextQuery : DbContext {
        public UserBoundedContextQuery(DbContextOptions<UserBoundedContextQuery> options)
        : base(options) {

        }

        #region DbSets
        public DbSet<User> Users { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
