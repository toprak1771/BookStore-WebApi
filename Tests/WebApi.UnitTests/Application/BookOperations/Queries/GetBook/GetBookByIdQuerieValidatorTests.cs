using System;
using System.Linq;
using TestSetup;
using Xunit;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Entities;
using WebApi.BookOperations.GetBooks;
using FluentAssertions;

namespace Application.BookOperations.Queries.GetBook
{
    public class GetBookByIdQuerieValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-2)]
        [InlineData(0)]
        public void WhenInvalidUnputsAreGiven_Validator_ShouldBeReturnsErrors(int id)
        {
           //arrange
           GetById querie=new GetById(null,null);
           querie.BookId=id;

            //act
            GetBookValidator validator = new GetBookValidator();
            var result = validator.Validate(querie);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {

           //arrange
           GetById querie=new GetById(null,null);
           querie.BookId=1;
           
            //act
            GetBookValidator validator = new GetBookValidator();
            var result = validator.Validate(querie);
            
            //assert
            result.Errors.Count.Should().Be(0);
        }
    }   
}