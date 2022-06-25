using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public int BookId { get; set; }

        public DeleteBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("Kitap bulunamadı!");
            }
            else
            {
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();
            }
        }
    }
}
