using System;
using System.Linq;
using TestSetup;
using Xunit;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Entities;
using WebApi.BookOperations.UpdateBook;
using FluentAssertions;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbcontext;
        
        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _dbcontext = testFixture.Context;
        }

        [Fact]
        public void WhenNotExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            int _bookId=100;
            UpdateBookCommand command = new UpdateBookCommand(_dbcontext);
            command.Model = new UpdateBookViewModel() {Title = "WhenNotExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 1000, GenreId=1};
            command.BookId = _bookId;

            //act & assert 
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap mevcut değil.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUptaded()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(_dbcontext);
            UpdateBookViewModel model= new UpdateBookViewModel() {Title="Hobbit", PageCount = 1000, GenreId=1, AuthorId=1};
            command.Model=model;
            command.BookId=1;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var book=_dbcontext.Books.SingleOrDefault(book=>book.Id == command.BookId);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.Title.Should().Be(model.Title);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);
        }

        
    }
}

