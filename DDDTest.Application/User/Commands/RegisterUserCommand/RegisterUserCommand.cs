using DDDTest.Domain.Aggregates.UserAggregate.Dtos;
using DDDTest.Domain.Framework.Messaging;
using MediatR;

namespace DDDTest.Application.User.Commands.RegisterUserCommand;
public class RegisterUserCommand : ICommand<long> {
    #region Constructors

    public RegisterUserCommand(RegisterUserDto registerUserDto) {
        RegisterUserDto = registerUserDto;
    }

    #endregion
    #region Properties
    public RegisterUserDto RegisterUserDto { get; set; }
    #endregion
}

