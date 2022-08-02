using DDDTest.DataAccess.Context;
using DDDTest.Domain.Aggregates.UserAggregate.Dtos;
using DDDTest.Domain.Aggregates.UserAggregate.Repositories;
using DDDTest.Domain.Events;
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
    public class UserRepositoryQuery : Repository<Domain.Aggregates.UserAggregate.Entities.User>, IUserRepositoryQuery {
        #region Fields
        private readonly UserBoundedContextQuery _context;
        private readonly IMediator _mediator;
        private readonly EventStoreClient _eventStoreClient;
        public UserRepositoryQuery(UserBoundedContextQuery dbContext, IMediator mediator, EventStoreClient eventStoreClient, IHttpContextAccessor httpContextAccessor = null) : base(dbContext, mediator, eventStoreClient, httpContextAccessor) {
            _context = dbContext;
            _mediator = mediator;
            _eventStoreClient = eventStoreClient;
        }
        #endregion
        #region Constructors

        #endregion

        #region Methods

        public Task<bool> IsUserNameUnique(string userName, CancellationToken cancellationToken)
            => _context.Users.AnyAsync(u => u.UserName == userName,cancellationToken);


        public void CreateUser(UserAddedDomainEvent userAddedDomainEvent) {
            _context.Users.AddAsync(new Domain.Aggregates.UserAggregate.Entities.User(userAddedDomainEvent.UserName,
                userAddedDomainEvent.Password,userAddedDomainEvent.Avatar));
            
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers() {

            var query = from user in _context.Users
                        select new UserDto {
                            UserName = user.UserName,
                            Password  =user.Password,
                            Avatar=user.Avatar
                        };
            return await query.ToListAsync();
        }


        //public IQueryable<Domain.UserAggregate.Entities.User> GetUsers() {
        //    var orders = from x in _context.Users
        //                  
        //                 select x;

        //    return orders;
        //}
        #endregion
    }
}
