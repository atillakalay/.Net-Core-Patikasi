using AutoMapper;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Queries.GetByIdBook;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.UserOperations.Command.CreateUser;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, GetByIdBookQuery.GetByIdBookModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book, GetBooksQuery.BooksViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<CreateBooksCommand.CreateBookModel, Book>().ReverseMap();
            CreateMap<UpdateBookViewCommand.UpdateBookModel, Book>().ReverseMap();
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<User, CreateUserModel>().ReverseMap();
        }
    }
}
