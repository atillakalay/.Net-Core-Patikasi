using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Queries.GetByIdBook;
using WebApi.DBOperations;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        //private static List<Book> BookList = new List<Book>()
        //{

        //        new Book
        //        {
        //            Id = 1,
        //            GenreId = 1,
        //            PageCount = 250,
        //            PublishDate = new DateTime(2001, 06, 12),
        //            Title = "Lean Startup"
        //        },
        //        new Book
        //        {
        //            Id = 2,
        //            GenreId = 2,
        //            PageCount = 350,
        //            PublishDate = new DateTime(2010, 05, 23),
        //            Title = "Herland"
        //        },
        //        new Book
        //        {
        //        Id = 3,
        //        GenreId = 3,
        //        PageCount = 540,
        //        PublishDate = new DateTime(2002, 12, 21),
        //        Title = "Dune"
        //    }
        //};

        public BookController(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            GetBooksQuery booksQuery = new GetBooksQuery(_dbContext, _mapper);
            var result = booksQuery.Handle();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetByIdBookQuery.GetByIdBookModel result;
            GetByIdBookQueryValidator validator = new GetByIdBookQueryValidator();
            GetByIdBookQuery query = new GetByIdBookQuery(_dbContext);
            query.BookId = id;
            validator.ValidateAndThrow(query);
            result = query.Handle();



            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBooksCommand.CreateBookModel newBook)
        {
            CreateBooksCommand booksCommand = new CreateBooksCommand(_dbContext, _mapper);

            booksCommand.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(booksCommand);
            booksCommand.Handle();
            return Ok();


            //if (!result.IsValid)
            //{
            //    foreach (var item in result.Errors)
            //    {
            //        Console.WriteLine("Özellik: " + item.PropertyName + " " + item.ErrorMessage);
            //    }
            //}
            //else
            //{
            //    booksCommand.Handle();
            //}
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookViewCommand.UpdateBookModel updatedBook)
        {

            UpdateBookViewCommand updateViewCommand = new UpdateBookViewCommand(_dbContext);
            UpdateBookViewCommandValidator validator = new UpdateBookViewCommandValidator();
            updateViewCommand.Id = id;
            updateViewCommand.Model = updatedBook;

            validator.ValidateAndThrow(updateViewCommand);
            updateViewCommand.Handle();

            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand deleteBookCommand = new DeleteBookCommand(_dbContext);
            deleteBookCommand.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(deleteBookCommand);
            deleteBookCommand.Handle();

            return Ok();
        }

        //[HttpGet]
        //public Book Get([FromQuery] string id)
        //{
        //    var book = BookList.SingleOrDefault(b => b.Id.ToString() == id);
        //    return book;
        //}
    }
}