using FluentValidation;
using System;

namespace WebApi.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommandValidator : AbstractValidator<CreateTokenCommand>
    {
        public CreateTokenCommandValidator()
        {
            RuleFor(command => command.Model.Password).NotEmpty();
            RuleFor(command => command.Model.Email).NotEmpty().EmailAddress();
        }
    }
}