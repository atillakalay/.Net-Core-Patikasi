using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookViewCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public UpdateBookModel Model { get; set; }
        public int Id { get; set; }

        public UpdateBookViewCommand(IBookStoreDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == Id);
            if (book is null)
            {
                throw new InvalidOperationException("Lütfen bir değer seçin!");
            }
            else
            {
                book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
                book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
                book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
                book.Title = Model.Title != default ? Model.Title : book.Title;
                _dbContext.SaveChanges();
            }
        }

        public class UpdateBookModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}
