using DDDTest.Domain.Aggregates.UserAggregate.DomainServices;
using DDDTest.Domain.Aggregates.UserAggregate.Repositories;
using Boilerplate.AspNetCore;

namespace DDDTest.Application.User.DomainServices;
public class UserDomainServices :  IUserDomainServices {
    private readonly IUserRepositoryQuery _userRepositoryQuery;
    public UserDomainServices(IUserRepositoryQuery userRepositoryQuery) {
        _userRepositoryQuery = userRepositoryQuery;
    }
    public Task<bool> IsUserNameUnique(string userName, CancellationToken cancellationToken)
        => _userRepositoryQuery.IsUserNameUnique(userName, cancellationToken);
}
