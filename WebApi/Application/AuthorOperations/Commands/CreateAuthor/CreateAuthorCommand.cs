using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorViewModel Model {get; set;}
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.Where(x=> x.Name == Model.Name).SingleOrDefault();
            if(author is not null)
            {
                throw new InvalidOperationException("Yazar zaten mevcut");
            }
            author = _mapper.Map<Author>(Model);
            _context.Authors.Add(author);
            _context.SaveChanges();
        }


        public class CreateAuthorViewModel
        {
             public string Name { get; set; }
             public string Surname { get; set; }
             public DateTime BornDate { get; set; }
        }
    }
}
