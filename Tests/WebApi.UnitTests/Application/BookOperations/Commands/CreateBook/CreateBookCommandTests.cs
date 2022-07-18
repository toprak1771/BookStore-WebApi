using TestSetup;
using Xunit;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Entities;
using WebApi.BookOperations.CreateBook;
using FluentAssertions;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using System;
using System.Linq;

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
            var book = new Book() { Title="Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate = new System.DateTime(1990,03,17), GenreId = 1,AuthorId=1};
            _dbcontext.Books.Add(book);
            _dbcontext.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_dbcontext, _mapper);
            command.Model=new CreateBookViewModel(){ Title = book.Title};

            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut.");
         }

         [Fact]
         public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
         {
            //arrange
            CreateBookCommand command = new CreateBookCommand(_dbcontext,_mapper);
            CreateBookViewModel model=new CreateBookViewModel() {Title = "Hobbit", PageCount=1000, PublishDate=DateTime.Now.Date.AddYears(-10), GenreId=1, AuthorId=1};
            command.Model=model;
            
            //act (Should ile kontrol etmezsen otomatik çalışmaz, invoke metodu ile çalıştırman lazım.)
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            
            //assert
            var book=_dbcontext.Books.SingleOrDefault(book=> book.Title == model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);
         }
    }
}