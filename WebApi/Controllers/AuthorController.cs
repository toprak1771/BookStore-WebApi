using System;
using System.Linq;
using System.Collections.Generic;
using FluentValidation;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]

    public class AuthorController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Authors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context,_mapper);
            return Ok(query.Handle());
        }
    }

}