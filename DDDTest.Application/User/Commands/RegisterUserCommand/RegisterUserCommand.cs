using DDDTest.Domain.Aggregates.UserAggregate.Dtos;
using DDDTest.Domain.Framework.Messaging;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DDDTest.Application.User.Commands.RegisterUserCommand;
public class RegisterUserCommand : ICommand<long> {
    #region Constructors
    public RegisterUserCommand() {

    }


    #endregion
    #region Properties
    //public RegisterUserDto RegisterUserDto { get; set; }

    public string UserName { get; set; } 

    public RegisterUserCommand(string userName, string password, IFormFile avatar) {
        UserName = userName;
        Password = password;
        Avatar = avatar;
    }

    public string Password { get; set; } 
    public IFormFile Avatar { get; set; }
    #endregion
}

