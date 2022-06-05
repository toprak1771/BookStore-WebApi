using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;
using Microsoft.EntityFrameworkCore;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbcontext;
        public DeleteBookCommand(BookStoreDbContext dbcontext)
        {
            _dbcontext=dbcontext;
        }

        public void Handle(int id)
        {
            var book=_dbcontext.Books.Find(id);
            if(book==null){
                throw new InvalidOperationException("Kitap mevcut değil.");
            }
            _dbcontext.Books.Remove(book);
            _dbcontext.SaveChanges();
        }
    }
}