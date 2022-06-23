using FluentValidation;

namespace WebApi.Application.BookOperations.Queries.GetByIdBook
{
    public class GetByIdBookQueryValidator : AbstractValidator<GetByIdBookQuery>
    {
        public GetByIdBookQueryValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0).NotEmpty();
        }
    }
}
