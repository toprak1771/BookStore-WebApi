using FluentValidation;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBookValidator : AbstractValidator<GetById>
    
    {
        public GetBookValidator()
        {
            RuleFor(command => command.BookId).NotEmpty().GreaterThan(0);
        }
        
    }
}