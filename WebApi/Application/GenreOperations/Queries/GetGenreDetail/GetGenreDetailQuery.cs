using WebApi.DBOperations;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        

        public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre=_context.Genres.Include(x=>x.Books).ThenInclude(a=> a.Author).Where(x=> x.IsActive && x.Id==GenreId).SingleOrDefault();
            if(genre is null)
            {
                throw new InvalidOperationException("Kitap türü bulunamadı.");
            }
            return _mapper.Map<GenreDetailViewModel>(genre);         
        }

    }


    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books {get; set;}
          
    }
}