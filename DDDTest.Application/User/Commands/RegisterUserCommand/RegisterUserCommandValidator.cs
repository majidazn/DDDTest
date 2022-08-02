using FluentValidation;

namespace DDDTest.Application.User.Commands.RegisterUserCommand;
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand> {
    public RegisterUserCommandValidator() {
        RuleFor(i => i.UserName).NotEmpty().NotNull();//.WithMessage("UserName2 is mandatory");
        RuleFor(i => i.Password).NotEmpty().NotNull().WithMessage("Password is mandatory");
        RuleFor(i => i.Avatar).NotEmpty().NotNull().WithMessage("Avatar is mandatory");
    }
}

