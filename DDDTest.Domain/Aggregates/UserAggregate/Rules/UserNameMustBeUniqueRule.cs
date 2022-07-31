using DDDTest.Domain.Aggregates.UserAggregate.DomainServices;
using DDDTest.Domain.Framework.SeedWork;

namespace DDDTest.Domain.Aggregates.UserAggregate.Rules;
public class UserNameMustBeUniqueRule : IBusinessRule {
    private readonly IUserDomainServices _userDomainServices;
    private readonly CancellationToken _cancellationToken;
    private readonly string _userName;

    public UserNameMustBeUniqueRule(IUserDomainServices userDomainServices, string userName, CancellationToken cancellationToken= default) {
        _userDomainServices = userDomainServices;
        _userName = userName;
        _cancellationToken = cancellationToken;
    }

    public string Message => "The username must be unique";

    public bool IsBroken() => _userDomainServices.IsUserNameUnique(_userName, _cancellationToken).Result;
}

