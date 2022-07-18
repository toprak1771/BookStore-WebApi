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
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,"Lord Of The Rings",0, 0, 0)]
        [InlineData(1,"Lord Of The Rings",0, 1, 0)]
        [InlineData(0,"Lord Of The Rings",100, 0, 0)]
        [InlineData(0,"",0, 0, 0)]
        [InlineData(1,"",100, 1, 1)]
        [InlineData(1,"",0, 1, 1)]
        [InlineData(0,"Lor",100, 1, 1)]
        [InlineData(1,"Lord",100, 0, 0)]
        [InlineData(1,"Lord",0, 1, 1)]
        [InlineData(0,"Lord",100, 1, 1)]
        [InlineData(1,"Lor",100, 1, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id,string title,int pageCount,int genreId,int authorId)
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookViewModel()
            {
                Title = title,
                PageCount = pageCount,
                GenreId = genreId,
                AuthorId = authorId
            };
            command.BookId = id;

            //act
            UpdateBookValidator validator = new UpdateBookValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model=new UpdateBookViewModel()
            {
                Title="Hobbit",
                PageCount = 1000,
                GenreId=1,
                AuthorId=1
            };
            command.BookId=10;
            
            //act
            UpdateBookValidator validator = new UpdateBookValidator();
            var result =validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }

    }
}