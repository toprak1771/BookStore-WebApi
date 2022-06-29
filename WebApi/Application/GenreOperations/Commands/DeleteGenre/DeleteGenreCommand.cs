using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _context;

        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context=context;
        }

        public void Handle()
        {
            var genre=_context.Genres.SingleOrDefault(x=> x.Id==GenreId);
            if(genre==null){
                throw new InvalidOperationException("Böyle bir kitap türü bulunamadı.");
            }

            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}