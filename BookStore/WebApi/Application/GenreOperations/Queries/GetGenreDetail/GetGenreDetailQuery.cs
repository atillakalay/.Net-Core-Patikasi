using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{

    public class GetGenreDetailQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int GenreId { get; set; }

        public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.SingleOrDefault(g => g.IsActive == true && g.Id == GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Kitap Türü Bulunamadı.");
            }
            return _mapper.Map<GenreDetailViewModel>(genre);
        }
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
