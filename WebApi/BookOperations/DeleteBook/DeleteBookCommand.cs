using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        public int BookId { get; set; }
        private readonly IBookStoreDbContext _dbcontext;
        public DeleteBookCommand(IBookStoreDbContext dbcontext)
        {
            _dbcontext=dbcontext;
        }

        public void Handle()
        {
            var book=_dbcontext.Books.SingleOrDefault(x=>x.Id==BookId);
            if(book==null){
                throw new InvalidOperationException("Kitap mevcut değil.");
            }
            _dbcontext.Books.Remove(book);
            _dbcontext.SaveChanges();
        }
    }
}