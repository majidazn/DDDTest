using DDDTest.Domain.Aggregates.UserAggregate.Dtos;
using DDDTest.Domain.Events;
using DDDTest.Domain.Framework.Repositories;

namespace DDDTest.Domain.Aggregates.UserAggregate.Repositories;
public interface IUserRepositoryQuery : IRepository<Entities.User> {
    Task<bool> IsUserNameUnique(string userName, CancellationToken cancellationToken);
    Task<IEnumerable<UserDto>> GetAllUsers();
   void CreateUser(UserAddedDomainEvent userAddedDomainEvent);
}

