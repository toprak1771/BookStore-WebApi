using WebApi.DBOperations;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author=_context.Authors.Include(x=>x.Books).Where(x=>x.Id == AuthorId).SingleOrDefault();
            if(author is null)
            {
                throw new InvalidOperationException("Yazar mevcut değil.");
            }

            AuthorDetailViewModel newAuthor=_mapper.Map<AuthorDetailViewModel>(author);
            return newAuthor;
        }


    }

    public class AuthorDetailViewModel
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname {get; set;}
            public string BornDate {get; set;}
            public List<Book> Books {get; set;}
            
    }
}