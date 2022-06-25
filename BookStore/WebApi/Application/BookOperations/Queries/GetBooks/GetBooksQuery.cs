using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Queries.GetBooks
{
    public class GetBooksQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<BooksViewModel> Handle()
        {
            var booklist = _dbContext.Books.Include(x => x.Genre).OrderBy(x => x.Id).ToList();
            List<BooksViewModel> viewModels = _mapper.Map<List<BooksViewModel>>(booklist);
            return viewModels;
        }

        public class BooksViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre { get; set; }
        }
    }
}