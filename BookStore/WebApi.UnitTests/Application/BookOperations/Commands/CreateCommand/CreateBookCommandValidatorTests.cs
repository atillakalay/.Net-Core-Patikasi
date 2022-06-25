using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateCommand
{

    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord Of The Rings", 0, 0)]
        [InlineData("Lord Of The Rings", 0, 1)]
        [InlineData("", 0, 0)]
        [InlineData("", 100, 1)]
        [InlineData("", 0, 1)]
        [InlineData("Lor", 100, 1)]
        [InlineData("Lor", 0, 0)]
        [InlineData("Lord", 0, 1)]
        public void WhenInvalidInputAreGivenValidator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            //arrange
            CreateBooksCommand command = new CreateBooksCommand(null, null);
            command.Model = new CreateBooksCommand.CreateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId
            };

            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBooksCommand command = new CreateBooksCommand(null, null);
            command.Model = new CreateBooksCommand.CreateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputGiven_Validator_ShouldNotBeReturnError()
        {
            CreateBooksCommand command = new CreateBooksCommand(null, null);
            command.Model = new CreateBooksCommand.CreateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}
