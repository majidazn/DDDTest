using DDDTest.DataAccess.Context;
using DDDTest.Domain.Aggregates.UserAggregate.Dtos;
using DDDTest.Domain.Aggregates.UserAggregate.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDTest.DataAccess.Repositories.User {
    public class UserRepositoryQuery : ReadRepository<Domain.Aggregates.UserAggregate.Entities.User>, IUserRepositoryQuery {
        #region Fields
        private readonly UserBoundedContextQuery _context;
        #endregion
        #region Constructors
        public UserRepositoryQuery(UserBoundedContextQuery userBoundedContextQuery) {
            _context = userBoundedContextQuery;
        }
        #endregion

        #region Methods

        public Task<bool> IsUserNameUnique(string userName, CancellationToken cancellationToken)
            => _context.Users.AnyAsync(u => u.UserName == userName,cancellationToken);

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
