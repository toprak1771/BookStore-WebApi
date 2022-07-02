using WebApi.DBOperations;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.Entities;
using System.Threading.Tasks;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery

    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var authors=_context.Authors.Include(x=>x.Books).OrderBy(x=>x.Id).ToList();
            List<AuthorsViewModel> returnList = _mapper.Map<List<AuthorsViewModel>>(authors);
            return returnList;
        }

       
    }

     public class AuthorsViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname {get; set;}
            public List<Book> Books {get; set;}
        }
    
}