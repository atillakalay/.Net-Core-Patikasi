using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

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
                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    }, new Genre
                    {
                        Name = "Romance"
                    }
                );

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

                context.AddRange(
                    new Author
                    {
                        Id = 1,
                        FirstName = "Atilla",
                        LastName = "Kalay",
                        BirthDate = new DateTime(1970, 11, 26)
                    },
                      new Author
                      {
                          Id = 2,
                          FirstName = "Rabia",
                          LastName = "Tanış",
                          BirthDate = new DateTime(1970, 11, 26)
                      },
                        new Author
                        {
                            Id = 3,
                            FirstName = "Yüşa",
                            LastName = "Balcı",
                            BirthDate = new DateTime(1970, 11, 26)
                        },
                          new Author
                          {
                              Id = 4,
                              FirstName = "Seda",
                              LastName = "Çakmak",
                              BirthDate = new DateTime(1970, 11, 26)
                          }
                    );
                context.SaveChanges();
            }
        }
    }
}
