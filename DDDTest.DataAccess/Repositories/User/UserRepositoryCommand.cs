using DDDTest.DataAccess.Context;
using DDDTest.Domain.Aggregates.UserAggregate.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDTest.DataAccess.Repositories.User {
    public class UserRepositoryCommand : WriteRepository<Domain.Aggregates.UserAggregate.Entities.User>, IUserRepositoryCommand {
        private readonly UserBoundedContextCommand _userBoundedContextCommand;
        public UserRepositoryCommand(UserBoundedContextCommand dbContext, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, httpContextAccessor) {
            _userBoundedContextCommand= dbContext;
        }
    }
}
