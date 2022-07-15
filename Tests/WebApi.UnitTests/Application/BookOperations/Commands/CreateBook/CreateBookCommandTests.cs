using TestSetup;
using Xunit;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Entities;
using WebApi.BookOperations.CreateBook;
using FluentAssertions;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using System;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
         private readonly BookStoreDbContext _dbcontext;
         private readonly IMapper _mapper;

         public CreateBookCommandTests(CommonTestFixture testFixture)
         {
            _dbcontext = testFixture.Context;
            _mapper = testFixture.Mapper;
         }
        
        
        [Fact]
         public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
         {
            //arrange
            var book = new Book() { Title="Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate = new System.DateTime(1990,03,17), GenreId = 1};
            _dbcontext.Books.Add(book);
            _dbcontext.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_dbcontext, _mapper);
            command.Model=new CreateBookViewModel(){ Title = book.Title};

            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut.");
         }
    }
}