using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;
using Microsoft.EntityFrameworkCore;

namespace WebApi.BookOperations.GetBooks
{
    public class GetById
    {
        private readonly BookStoreDbContext _dbcontext;
        //public int BookId {get;set;}
        public GetById(BookStoreDbContext dbcontext)
        {
            _dbcontext=dbcontext;
        }

        public GetByIdModel Handle(int id)
        {
            var book=_dbcontext.Books.Find(id);
            if(book==null){
                throw new InvalidOperationException("Kitap bulunamadı.");
            }
           GetByIdModel getBook=new GetByIdModel();
           getBook.Title=book.Title;
           getBook.PageCount=book.PageCount;
           getBook.PublishDate=book.PublishDate.Date.ToString("dd/mm/yyy");
           getBook.Genre=((GenreEnum)book.GenreId).ToString();
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