using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Command.UpdateGenreCommand
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        public UpdateGenreCommand(IBookStoreDbContext dbContextdbContext)
        {
            _dbContext = dbContextdbContext;
        }
        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Kitap Türü Bulunamadı");
            }

            if (_dbContext.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
            {
                throw new InvalidOperationException("Aynı isimli bir kitap türü zaten mevcut");
            }

            genre.Name = string.IsNullOrEmpty(Model.Name.ToLower()) ? genre.Name : Model.Name;
            genre.IsActive = Model.IsActive;
            _dbContext.SaveChanges();

        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
