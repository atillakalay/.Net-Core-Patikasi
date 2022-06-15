using Microsoft.AspNetCore.Mvc;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private static List<Book> BookList = new List<Book>()
        {
            new Book
            {
                Id = 1,
                GenreId = 1,
                PageCount = 250,
                PublishDate = new DateTime(2001, 06, 12),
                Title = "Lean Startup"
            },
            new Book
            {
                Id = 2,
                GenreId = 2,
                PageCount = 350,
                PublishDate = new DateTime(2010, 05, 23),
                Title = "Herland"
            },
            new Book
            {
            Id = 3,
            GenreId = 3,
            PageCount = 540,
            PublishDate = new DateTime(2002, 12, 21),
            Title = "Dune"
        }
        };

        [HttpGet]
        public List<Book> GetAll()
        {
            var books = BookList.ToList();
            return books;
        }


        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = BookList.SingleOrDefault(b => b.Id == id);
            return book;
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);
            if (book is not null)
            {
                return BadRequest();
            }

            BookList.Add(newBook);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);
            if (book is null)
            {
                return BadRequest();
            }
            else
            {
                book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
                book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
                book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
                book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
                return Ok();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);
            if (book is null)
            {
                return BadRequest();
            }
            else
            {
                BookList.Remove(book);
                return Ok();
            }
        }

        //[HttpGet]
        //public Book Get([FromQuery] string id)
        //{
        //    var book = BookList.SingleOrDefault(b => b.Id.ToString() == id);
        //    return book;
        //}
    }
}