using DDDTest.Domain.Aggregates.UserAggregate.Entities;
using MediatR;

namespace DDDTest.Domain.Events {
    public class UserAddedDomainEvent: INotification {
        public string UserName { get; }
        public string Password { get; }
        public byte[] Avatar { get; }
        public UserAddedDomainEvent(string userName, string password,
                                    byte[] avatar) {
            UserName = userName;
            Password = password;
            Avatar = avatar;
        }
    }
}
