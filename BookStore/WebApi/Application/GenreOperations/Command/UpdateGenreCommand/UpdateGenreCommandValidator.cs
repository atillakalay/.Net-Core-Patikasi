using FluentValidation;

namespace WebApi.Application.GenreOperations.Command.UpdateGenreCommand
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(4).When(x => x.Model.Name != string.Empty);
        }
    }
}
