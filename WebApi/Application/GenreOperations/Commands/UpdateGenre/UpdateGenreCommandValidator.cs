using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command=>command.Model.Name).NotEmpty();
            RuleFor(command=>command.Model.IsActive).NotEmpty();
        }
    }
}