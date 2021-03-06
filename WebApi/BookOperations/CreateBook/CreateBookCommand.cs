using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookViewModel Model {get; set;}
        private readonly IMapper _mapper;
        private readonly IBookStoreDbContext _dbcontext;
        public CreateBookCommand(IBookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext=dbcontext;
            _mapper=mapper;
        }

        public void Handle()
        {
            
            var book=_dbcontext.Books.SingleOrDefault(x=>x.Title==Model.Title);
            if(book!=null){
                throw new InvalidOperationException("Kitap zaten mevcut.");
            }

            book=_mapper.Map<Book>(Model);
          
            _dbcontext.Books.Add(book);
            _dbcontext.SaveChanges();
        }

        public class CreateBookViewModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int AuthorId {get; set;}
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}