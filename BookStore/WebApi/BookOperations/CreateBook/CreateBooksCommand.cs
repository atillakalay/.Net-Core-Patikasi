using WebApi.DBOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBooksCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public CreateBookModel Model { get; set; }

        public CreateBooksCommand(BookStoreDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)
            {
                throw new InvalidOperationException("Kitap Zaten Mevcut");
            }
            else
            {
                book = new Book();
                book.Title = Model.Title;
                book.GenreId = Model.GenreId;
                book.PageCount = Model.PageCount;
                book.PublishDate=Model.PublishDate;
                _dbContext.Books.Add(book);
                _dbContext.SaveChanges();
            }
        }

        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}
