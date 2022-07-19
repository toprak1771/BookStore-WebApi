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
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-2)]
        [InlineData(0)]
        public void WhenInvalidUnputsAreGiven_Validator_ShouldBeReturnsErrors(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId=id;

            //act
            DeleteBookValidator validator = new DeleteBookValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = 1;

            DeleteBookValidator validator = new DeleteBookValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0); 
        }
    }
}