using FluentValidation;

namespace WebApi.Application.GenreOperations.Command.DeleteGenre
{
    public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).GreaterThan(0).NotEmpty();
        }
    }
}
