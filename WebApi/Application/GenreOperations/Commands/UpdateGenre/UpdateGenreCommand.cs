using System;
using System.Linq;
using WebApi.Entities;
using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;


namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public UpdateGenreViewModel Model {get; set;}
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _context;
        

        public UpdateGenreCommand(BookStoreDbContext context)
        {
            _context=context;
        }

        public void Handle()
        {
            var genre=_context.Genres.SingleOrDefault(x=> x.Id == GenreId);
            if(genre is null){
                throw new InvalidOperationException("Kitap türü bulunamadı.");
            }
            if(_context.Genres.Any(x=>x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
            {
                throw new InvalidOperationException("Aynı isimli kitap türü mevcut");
            }
            genre.Name=string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
            genre.IsActive = Model.IsActive;
            _context.SaveChanges();
        }
        
    }

    public class UpdateGenreViewModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}