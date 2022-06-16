using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetByIdBook;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _dbContext;
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

        public BookController(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            GetBooksQuery booksQuery = new GetBooksQuery(_dbContext);
            var result = booksQuery.Handle();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetByIdBookQuery.GetByIdBookModel result;
            try
            {
                GetByIdBookQuery query = new GetByIdBookQuery(_dbContext);
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBooksCommand.CreateBookModel newBook)
        {
            CreateBooksCommand booksCommand = new CreateBooksCommand(_dbContext);
            try
            {
                booksCommand.Model = newBook;
                booksCommand.Handle();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookViewCommand.UpdateBookModel updatedBook)
        {
            try
            {
                UpdateBookViewCommand updateViewCommand = new UpdateBookViewCommand(_dbContext);
                updateViewCommand.Id = id;
                updateViewCommand.Model = updatedBook;
                updateViewCommand.Handle();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand deleteBookCommand = new DeleteBookCommand(_dbContext);
                deleteBookCommand.BookId = id;
                deleteBookCommand.Handle();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

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