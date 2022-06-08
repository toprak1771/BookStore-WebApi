using FluentValidation;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
    
    {
        public UpdateBookValidator()
        {
            RuleFor(command => command.BookId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.Title).NotEmpty();
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
        }

    }
}