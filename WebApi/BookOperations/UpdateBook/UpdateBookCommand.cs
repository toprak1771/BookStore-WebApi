using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookViewModel Model{get;set;}
        public int BookId { get; set; }

        //public int BookId {get; set;}
        private readonly BookStoreDbContext _dbcontext;
        
        public UpdateBookCommand(BookStoreDbContext dbcontext)
        {
            _dbcontext=dbcontext;
            
        }

        public void Handle()
        {
            var book=_dbcontext.Books.SingleOrDefault(x=>x.Id == BookId);
            if(book==null){
                throw new InvalidOperationException("Kitap mevcut değil.");
            }
            

            book.Title=Model.Title != default ? Model.Title : book.Title;
            book.GenreId=Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount=Model.PageCount != default ? Model.PageCount : book.PageCount;
            
            _dbcontext.SaveChanges();
        }

        public class UpdateBookViewModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            
        }
    }
}