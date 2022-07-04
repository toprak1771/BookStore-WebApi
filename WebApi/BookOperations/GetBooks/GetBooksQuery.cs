using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;

        public GetBooksQuery(BookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext=dbcontext;
            _mapper=mapper;
        }

        public List<BookViewModel> Handle()
        {
            var bookList=_dbcontext.Books.Include(x=> x.Genre).Include(x=> x.Author).OrderBy(x=>x.Id).ToList<Book>();

            List<BookViewModel> bl=_mapper.Map<List<BookViewModel>>(bookList);
            
            return bl;

        }

        public class BookViewModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre{get; set;}
            public string Author {get; set;}
        }

    }
    
}