using Microsoft.EntityFrameworkCore;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Books.AddRange(
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
                    });
                context.SaveChanges();
            }
        }
    }
}
