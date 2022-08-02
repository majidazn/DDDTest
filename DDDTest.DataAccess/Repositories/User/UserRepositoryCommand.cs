using DDDTest.DataAccess.Context;
using DDDTest.Domain.Aggregates.UserAggregate.Repositories;
using EventStore.Client;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDTest.DataAccess.Repositories.User {
    public class UserRepositoryCommand : Repository<Domain.Aggregates.UserAggregate.Entities.User>, IUserRepositoryCommand {
        private readonly UserBoundedContextCommand _userBoundedContextCommand;
        private readonly IMediator _mediator;
        private readonly EventStoreClient _eventStoreClient;
        public UserRepositoryCommand(UserBoundedContextCommand dbContext,IMediator mediator,EventStoreClient eventStoreClient, IHttpContextAccessor httpContextAccessor = null) : base(dbContext,mediator, eventStoreClient, httpContextAccessor) {
            _userBoundedContextCommand= dbContext;
            _mediator= mediator;
            _eventStoreClient= eventStoreClient;
        }
    }
}
