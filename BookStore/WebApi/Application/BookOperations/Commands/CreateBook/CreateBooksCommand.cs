using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.CreateBook
{
    public class CreateBooksCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public CreateBookModel Model { get; set; }
        private readonly IMapper _mapper;

        public CreateBooksCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            _mapper = mapper;
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
                book = _mapper.Map<Book>(Model);
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
