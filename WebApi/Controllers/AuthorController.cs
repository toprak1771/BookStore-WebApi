using System;
using System.Linq;
using System.Collections.Generic;
using FluentValidation;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using static WebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;



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

        [HttpGet("{id}")]
        public IActionResult GetAuthorDetail(int id)
        {
            GetAuthorDetailQuery query=new GetAuthorDetailQuery(_context,_mapper);
            GetAuthorDetailQueryValidator validator= new GetAuthorDetailQueryValidator();

            query.AuthorId=id;
            validator.ValidateAndThrow(query);
            return Ok(query.Handle());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorCommandViewModel _updateAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            UpdateAuthorCommandValidator validator= new UpdateAuthorCommandValidator();

            command.AuthorId=id;
            command.Model=_updateAuthor;
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

      
    }

}