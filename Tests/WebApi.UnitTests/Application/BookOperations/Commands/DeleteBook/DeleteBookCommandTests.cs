using System;
using System.Linq;
using TestSetup;
using Xunit;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Entities;
using WebApi.BookOperations.DeleteBook;
using FluentAssertions;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbcontext;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _dbcontext = testFixture.Context;
        }

    [Fact]
    public void WhenNotExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        //arrange
        int _bookId = 100;

        DeleteBookCommand command = new DeleteBookCommand(_dbcontext);
        command.BookId = _bookId;

        //act & assert
        FluentActions.Invoking(() => command.Handle())
                     .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap mevcut değil.");
    }

    [Fact]
    public void WhenExistBookIdIsGiven_Book_ShouldBeDeleted()
    {
        //arrange
        var newBook=new Book 
        {
            Title = "WhenExistBookIdIsGiven_Book_ShouldBeDeleted",
            PageCount = 1000,
            PublishDate = DateTime.Now.Date.AddYears(-5),
            GenreId=1,
            AuthorId=1
        };
        _dbcontext.Books.Add(newBook);
        _dbcontext.SaveChanges();

        DeleteBookCommand command = new DeleteBookCommand(_dbcontext);
        command.BookId = newBook.Id;

        //act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        //assert
        var book=_dbcontext.Books.SingleOrDefault(b=>b.Title==newBook.Title);
        book.Should().BeNull();
        
    }

    }
}

