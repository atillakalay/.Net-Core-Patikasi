using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.UpdateBook;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBooksCommand.CreateBookModel, Book>().ReverseMap();
            CreateMap<UpdateBookViewCommand.UpdateBookModel, Book>().ReverseMap();
        }
    }
}
