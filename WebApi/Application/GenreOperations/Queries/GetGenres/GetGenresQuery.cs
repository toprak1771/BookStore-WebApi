using WebApi.DBOperations;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.Entities;
using System.Threading.Tasks;

namespace WebApi.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenresQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genres=_context.Genres.Include(x=> x.Books).ThenInclude(x=>x.Author).Where(x=> x.IsActive).OrderBy(x=> x.Id).ToList();
            List<GenresViewModel> returnList = _mapper.Map<List<GenresViewModel>>(genres);
            return returnList;
        }

    }


    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books {get; set;}
         
    }
}