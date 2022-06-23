using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBooksCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.PageCount).GreaterThan(0).NotEmpty();
            RuleFor(command => command.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date).NotEmpty();
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
        }
    }
}
