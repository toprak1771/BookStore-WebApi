using WebApi.DBOperations;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;

namespace WebApi.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenresQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genres=_context.Genres.Where(x=> x.IsActive).OrderBy(x=> x.Id);
            List<GenresViewModel> returnList = _mapper.Map<List<GenresViewModel>>(genres);
            return returnList;
        }

    }


    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }  
    }
}