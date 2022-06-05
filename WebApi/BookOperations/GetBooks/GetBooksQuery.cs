using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;
using Microsoft.EntityFrameworkCore;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbcontext;

        public GetBooksQuery(BookStoreDbContext dbcontext)
        {
            _dbcontext=dbcontext;
        }

        public List<BookViewModel> Handle()
        {
            var bookList=_dbcontext.Books.OrderBy(x=>x.Id).ToList<Book>();
            List<BookViewModel> bl=new List<BookViewModel>();
            foreach (var book in bookList)
            {
                bl.Add(new BookViewModel()
                {
                    Title=book.Title,
                    PageCount=book.PageCount,
                    PublishDate=book.PublishDate.Date.ToString("dd/MM/yyy"),
                    Genre=((GenreEnum)book.GenreId).ToString()
                });
            };
            return bl;

        }

        public class BookViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre{get; set;}
        }

    }
    
}