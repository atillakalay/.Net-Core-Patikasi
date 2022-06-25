using WebApi.DBOperations;

namespace WebApi.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {

            context.Books.AddRange(
                new Entities.Book
                {
                    Id = 1,
                    GenreId = 1,
                    PageCount = 250,
                    PublishDate = new DateTime(2001, 06, 12),
                    Title = "Lean Startup"
                },
                new Entities.Book
                {
                    Id = 2,
                    GenreId = 2,
                    PageCount = 350,
                    PublishDate = new DateTime(2010, 05, 23),
                    Title = "Herland"
                },
                new Entities.Book
                {
                    Id = 3,
                    GenreId = 3,
                    PageCount = 540,
                    PublishDate = new DateTime(2002, 12, 21),
                    Title = "Dune"
                });
        }
    }
}
