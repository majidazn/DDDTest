using DDDTest.Domain.Aggregates.UserAggregate.DomainServices;
using DDDTest.Domain.Aggregates.UserAggregate.Rules;
using DDDTest.Domain.Events;
using DDDTest.Domain.Framework.Exceptions;
using DDDTest.Domain.Framework.SeedWork;
using Microsoft.AspNetCore.Http;

namespace DDDTest.Domain.Aggregates.UserAggregate.Entities;
public class User : Entity, IAggregateRoot {
    #region Constructor
    public User() {

    }
 
    public User(
                string userName,
                string password,
                byte[] avatar) {
        UserName = userName;
        Password = password;
        Avatar = avatar;


    }


    #endregion
    #region Properties
    public string UserName { get; private set; }
    public string Password { get; private set; }
    public byte[] Avatar { get; private set; }


    #endregion

    #region Behaviors
    public static User Register(IUserDomainServices userDomainServices, string userName, string password, byte[] avatar) {
        EnforceInvariants(userName, password, avatar);
        CheckRule(new UserNameMustBeUniqueRule(userDomainServices, userName));
        AddDomainEvent(new UserAddedDomainEvent(userName, password, avatar));
       // _domainEvents.Add(new UserAddedDomainEvent(userName, password, avatar));
        return new User(userName, password, avatar);
    }

    public void ChangePassword(string newPassword) {
        Password = newPassword;
    }
    #endregion

    #region Invariants
    private static void EnforceInvariants(string userName, string password, byte[] avatar) {
        if (avatar.Equals(null))
            throw new AppException("Avatar is mandatory");

        if (avatar.Length > 1000000)
            throw new AppException("Avatar must be less than 1MB");

    }
    #endregion
}

