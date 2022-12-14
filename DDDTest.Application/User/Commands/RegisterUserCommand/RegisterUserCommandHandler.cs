using DDDTest.Application.Infrastrutures;
using DDDTest.Domain.Aggregates.UserAggregate.DomainServices;
using DDDTest.Domain.Aggregates.UserAggregate.Repositories;
using DDDTest.Domain.Framework.SeedWork;
using MediatR;

namespace DDDTest.Application.User.Commands.RegisterUserCommand;
public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, long>  {
    #region Fields
    private readonly IUserDomainServices _userDomainServices;
    private readonly IUserRepositoryCommand _userRepositoryCommand;
    #endregion

    #region Constructors
    public RegisterUserCommandHandler(IUserDomainServices userDomainServices, IUserRepositoryCommand userRepositoryCommand) {
        _userDomainServices = userDomainServices;
        _userRepositoryCommand = userRepositoryCommand;
    }
    #endregion
    public async Task<long> Handle(RegisterUserCommand request, CancellationToken cancellationToken) {

        var password = request.Password;
        var avatar = request.Avatar.ToBytes();




        var user =Domain.Aggregates.UserAggregate.Entities.User.Register(
            _userDomainServices, request.UserName, password, avatar);

        await _userRepositoryCommand.CreateAsync(user, cancellationToken);
        await _userRepositoryCommand.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}

