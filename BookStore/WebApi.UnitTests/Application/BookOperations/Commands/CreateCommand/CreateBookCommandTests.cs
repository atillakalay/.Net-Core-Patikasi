using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateCommand
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistBookTitleGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)

            var book = new Book()
            {
                Title = "WhenAlreadyExistBookTitleGiven_InvalidOperationException_ShouldBeReturn",
                PageCount = 100,
                PublishDate = new System.DateTime(1990, 01, 10),
                GenreId = 1
            };
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

            CreateBooksCommand command = new CreateBooksCommand(_dbContext, _mapper);
            command.Model = new CreateBooksCommand.CreateBookModel() { Title = book.Title };

            //act & assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking((() => command.Handle())).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Zaten Mevcut");

        }

        [Fact]
        public void WhenValidInpıtsAreGiven_Book_ShouldBeCreated()
        {
            //arrange
            CreateBooksCommand command = new CreateBooksCommand(_dbContext, _mapper);
            CreateBooksCommand.CreateBookModel model = new CreateBooksCommand.CreateBookModel() { Title = "Hobbit", PageCount = 1000, PublishDate = DateTime.Now.AddYears(-10), GenreId = 1 };
            command.Model = model;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var book = _dbContext.Books.SingleOrDefault(book => book.Title == model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
        }

    }
}
