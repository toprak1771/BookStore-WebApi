using TestSetup;
using Xunit;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Entities;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using FluentAssertions;
using static WebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using System;
using System.Linq;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _dbcontext=testFixture.Context;
            _mapper=testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthorIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            var author = new Author(){ Name="WhenAlreadyExistAuthorIsGiven_InvalidOperationException_ShouldBeReturn", Surname="Test", BornDate=new DateTime(1985,10,22) };
            _dbcontext.Authors.Add(author);
            _dbcontext.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_dbcontext,_mapper);
            command.Model=new CreateAuthorViewModel() {Name="WhenAlreadyExistAuthorIsGiven_InvalidOperationException_ShouldBeReturn"};

            //act & assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            //arrange 
            CreateAuthorCommand command = new CreateAuthorCommand(_dbcontext,_mapper);
            CreateAuthorViewModel model = new CreateAuthorViewModel() {Name="Test", Surname="TestTest", BornDate=new DateTime(1997,10,22)};
            command.Model=model;

            //act
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            //assert
            var author = _dbcontext.Authors.SingleOrDefault(author => author.Name == model.Name);
            author.Should().NotBeNull();
            author.Surname.Should().Be(model.Surname);
            author.BornDate.Should().Be(model.BornDate);
        }
        

        
    }

}