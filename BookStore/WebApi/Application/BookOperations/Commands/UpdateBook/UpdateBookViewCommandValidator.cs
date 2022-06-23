using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookViewCommandValidator : AbstractValidator<UpdateBookViewCommand>
    {
        public UpdateBookViewCommandValidator()
        {
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.PageCount).GreaterThan(0).NotEmpty();
            RuleFor(command => command.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date).NotEmpty();
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
        }
    }
}
