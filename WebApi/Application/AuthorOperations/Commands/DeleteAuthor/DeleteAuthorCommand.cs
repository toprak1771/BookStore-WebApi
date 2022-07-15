using System;
using System.Linq;
using WebApi.Entities;
using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _context;

        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author=_context.Authors.Include(x=>x.Books).Where(x=>x.Id == AuthorId).SingleOrDefault();
            if(author is null){
                throw new InvalidOperationException("Yazar mevcut değil.");
            }
            if(author.Books.Any()){ 
                throw new InvalidOperationException("Yazarın kitapları mevcut, lütfen önce kitapları siliniz.");
            }
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}