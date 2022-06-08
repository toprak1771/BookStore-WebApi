using System;
using FluentValidation;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookValidator : AbstractValidator<CreateBookCommand>
    {
       public CreateBookValidator()
       {
            RuleFor(command => command.Model.Title).NotEmpty();
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            RuleFor(command => command.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);
       }

    }
}