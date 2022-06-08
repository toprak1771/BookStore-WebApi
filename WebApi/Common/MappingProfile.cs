using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
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
                                            .ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=> ((GenreEnum)src.GenreId).ToString()))
                                            .ForMember(dest=>dest.PublishDate, opt=>opt.MapFrom(src=> (src.PublishDate.Date).ToString("dd/MM/yyy")));
            CreateMap<Book, BookViewModel>()
                                            .ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()))
                                            .ForMember(dest=>dest.PublishDate, opt=>opt.MapFrom(src=> (src.PublishDate.Date).ToString("dd/MM/yyy")));
            
                                            

                                            
        }
    }
    
}