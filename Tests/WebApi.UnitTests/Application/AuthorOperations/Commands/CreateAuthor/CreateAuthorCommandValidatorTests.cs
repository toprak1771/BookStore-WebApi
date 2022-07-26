using TestSetup;
using Xunit;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Entities;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using FluentAssertions;
using static WebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using System;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("","Test")]
        [InlineData("","")]
        [InlineData("Test","")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null,null);
            command.Model=new CreateAuthorViewModel() 
            {
               Name=name,
               Surname=surname,
               BornDate=DateTime.Now.Date.AddYears(-1)
            };

            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result=validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null,null);
            command.Model=new CreateAuthorViewModel() 
            {
               Name="TestTest",
               Surname="TestTestTest",
               BornDate=DateTime.Now.Date
            };

            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result=validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null,null);
            command.Model=new CreateAuthorViewModel() 
            {
               Name="TestTest",
               Surname="TestTestTest",
               BornDate=DateTime.Now.Date.AddYears(-2)
            };

            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result=validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }



    }
}


