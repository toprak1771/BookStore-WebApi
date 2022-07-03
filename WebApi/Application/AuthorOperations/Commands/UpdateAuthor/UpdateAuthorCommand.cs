using System;
using System.Linq;
using WebApi.Entities;
using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorCommandViewModel Model {get; set;}
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _context;
        
        public UpdateAuthorCommand(BookStoreDbContext context)
        {
            _context=context;
        }

        public void Handle()
        {
            var author=_context.Authors.Where(x=>x.Id == AuthorId).SingleOrDefault();
            if(author is null)
            {
                throw new InvalidOperationException("Böyle bir yazar bulunamadı.");
            }
            if(_context.Authors.Any(x=>x.Name.ToLower() == Model.Name.ToLower() && x.Id != AuthorId))
             {
                throw new InvalidOperationException("Aynı isimli kitap türü mevcut");
            }
            author.Name=string.IsNullOrEmpty(Model.Name.Trim()) ? author.Name : Model.Name;
            author.Surname=string.IsNullOrEmpty(Model.Surname.Trim()) ? author.Surname : Model.Surname;
            author.BornDate = Model.BornDate == default ? author.BornDate : Model.BornDate;
            _context.SaveChanges();
        }
    }

    public class UpdateAuthorCommandViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BornDate {get; set;}
    }
}