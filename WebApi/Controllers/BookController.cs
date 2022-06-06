using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations.DeleteBook;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;


namespace WebApi.AddControllers{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase 
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }

        
    
        [HttpGet]
        public IActionResult Books()
        {
            GetBooksQuery query=new GetBooksQuery(_context);
            var result=query.Handle();
            return Ok(result);
        }
    
       [HttpGet("{id}")]
       public IActionResult GetById(int id)
        {
            GetById getById=new GetById(_context);
            try
            {
               var result=getById.Handle(id);
               return Ok(result);
            }
            catch (Exception ex)
            {
                
               return BadRequest(ex.Message);
            }
            
        }

        // [HttpGet]
        // public Book getBooks([FromQuery] string id){
        //     var book=BookList.Where(c=>c.Id==Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

         [HttpPost]
         public IActionResult AddBook([FromBody] CreateBookViewModel newBook)
         {
             CreateBookCommand command=new CreateBookCommand(_context, _mapper);
             try
             {
                command.Model=newBook;
                command.Handle();
             }
             catch (Exception ex)
             {
                return BadRequest(ex.Message);
                 
             }

             return Ok();
             
         }

        [HttpPut("{id}")]
        public IActionResult UptadeBook(int id,[FromBody] UpdateBookViewModel updateBook)
        {
            UpdateBookCommand command=new UpdateBookCommand(_context);
            try
            {
                command.Model=updateBook;
                command.Handle(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
            
        }  

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command=new DeleteBookCommand(_context);
            try
            {
                command.Handle(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

    }
}