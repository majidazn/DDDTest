using DDDTest.Domain.Aggregates.UserAggregate.Dtos;
using DDDTest.Domain.Aggregates.UserAggregate.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDTest.Application.User.Services {
    public class UserService : IUserService {
        private readonly IUserRepositoryQuery _userRepositoryQuery;
        public UserService(IUserRepositoryQuery userRepositoryQuery) {
            _userRepositoryQuery = userRepositoryQuery;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers() {
            return await _userRepositoryQuery.GetAllUsers();
        }
    }
}
