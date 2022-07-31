namespace DDDTest.Domain.Aggregates.UserAggregate.DomainServices;
public interface IUserDomainServices {
    Task<bool> IsUserNameUnique(string userName,CancellationToken cancellationToken);
}

