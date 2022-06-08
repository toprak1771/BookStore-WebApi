using FluentValidation;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookValidator()
        {
            RuleFor(command => command.BookId).NotEmpty().GreaterThan(0);
        }
    }
}