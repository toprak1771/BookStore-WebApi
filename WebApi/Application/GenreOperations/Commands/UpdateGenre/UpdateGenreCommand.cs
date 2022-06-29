using System;
using AutoMapper;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public UpdateGenreViewModel Model {get; set;}
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateGenreCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }

        public void Handle()
        {
            var genre=_context.Genres.SingleOrDefault(x=> x.Id==GenreId);
            if(genre is null){
                throw new InvalidOperationException("Kitap türü bulunamadı.");
            }
            genre=_mapper.Map<Genre>(Model);
            _context.SaveChanges();
        }
        
    }

    public class UpdateGenreViewModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}