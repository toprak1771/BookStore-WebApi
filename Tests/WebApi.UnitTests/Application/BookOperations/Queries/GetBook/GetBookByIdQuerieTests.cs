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
    public class GetBookByIdQuerieTests : IClassFixture<CommonTestFixture>
    {
         private readonly BookStoreDbContext _dbcontext;
         private readonly IMapper _mapper;
         public GetBookByIdQuerieTests(CommonTestFixture testFixture)
         {
            _dbcontext = testFixture.Context;
            _mapper = testFixture.Mapper;
         }

        [Fact]
        public void WhenNotExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            int _bookId=300;

            GetById querie=new GetById(_dbcontext,_mapper);
            querie.BookId=_bookId;

            //act&assert
            FluentActions.Invoking(() => querie.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı.");
        }

    }
}

