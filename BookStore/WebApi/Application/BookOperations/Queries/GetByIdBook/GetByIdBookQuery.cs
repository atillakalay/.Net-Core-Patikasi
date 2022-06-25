using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Queries.GetByIdBook
{
    public class GetByIdBookQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        public GetByIdBookModel Model { get; set; }
        public int BookId { get; set; }

        public GetByIdBookQuery(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public GetByIdBookModel Handle()
        {
            var book = _dbContext.Books.Include(x => x.Genre).SingleOrDefault(b => b.Id == BookId);
            if (book == null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }
            else
            {
                GetByIdBookModel getByIdBookModel = new GetByIdBookModel();
                getByIdBookModel.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
                getByIdBookModel.PageCount = book.PageCount;
                getByIdBookModel.Title = book.Title;
                getByIdBookModel.Genre = ((GenreEnum)book.GenreId).ToString();
                return getByIdBookModel;
            }


        }

        public class GetByIdBookModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Genre { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }
    }
}
