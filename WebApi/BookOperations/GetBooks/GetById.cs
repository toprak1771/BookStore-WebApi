using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;
using Microsoft.EntityFrameworkCore;

namespace WebApi.BookOperations.GetBooks
{
    public class GetById
    {
        public int BookId { get; set; }
        private readonly BookStoreDbContext _dbcontext;
        private readonly IMapper _mapper; 
        //public int BookId {get;set;}
        public GetById(BookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext=dbcontext;
            _mapper=mapper;
        }

        public GetByIdModel Handle()
        {
            var book=_dbcontext.Books.SingleOrDefault(x=> x.Id == BookId);
            if(book==null){
                throw new InvalidOperationException("Kitap bulunamadı.");
            }
           GetByIdModel getBook=_mapper.Map<GetByIdModel>(book);
           
           return getBook;
        }

        public class GetByIdModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre { get; set; }
        }

    }
}