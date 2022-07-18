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
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord Of The Rings",0, 0, 0)]
        [InlineData("Lord Of The Rings",0, 1, 0)]
        [InlineData("Lord Of The Rings",100, 0, 0)]
        [InlineData("",0, 0, 0)]
        [InlineData("",100, 1, 1)]
        [InlineData("",0, 1, 1)]
        [InlineData("Lor",100, 1, 1)]
        [InlineData("Lord",100, 0, 0)]
        [InlineData("Lord",0, 1, 1)]
        //[InlineData("Lord",100, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId)
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookViewModel()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId,
                AuthorId = authorId
            };

            //act
            CreateBookValidator validator=new CreateBookValidator();
            var result=validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model=new CreateBookViewModel()
            {
                Title="Lord of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId=1,
                AuthorId=1
            };

            CreateBookValidator validator = new CreateBookValidator();
            var result =validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model=new CreateBookViewModel()
            {
                Title="Lord of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId=1,
                AuthorId=1
            };

            //act
            CreateBookValidator validator = new CreateBookValidator();
            var result =validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}