using WebApi.Entities;
using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using static WebApi.Application.GenreOperations.Queries.GetGenres.GetGenresQuery;
using static WebApi.Application.GenreOperations.Queries.GetGenreDetail.GetGenreDetailQuery;
using static WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.GetBooks.GetById;
using static WebApi.BookOperations.GetBooks.GetBooksQuery;


namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookViewModel, Book>();
            CreateMap<Book, GetByIdModel>()
                                            .ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=> src.Genre.Name))
                                            .ForMember(dest=>dest.PublishDate, opt=>opt.MapFrom(src=> (src.PublishDate.Date).ToString("dd/MM/yyy")));
            CreateMap<Book, BookViewModel>()
                                            .ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=> src.Genre.Name))
                                            .ForMember(dest=>dest.PublishDate, opt=>opt.MapFrom(src=> (src.PublishDate.Date).ToString("dd/MM/yyy")));
            
            CreateMap<Genre, GenresViewModel>();

            CreateMap<Genre, GenreDetailViewModel>();  

                                          

                                            
        }
    }
    
}