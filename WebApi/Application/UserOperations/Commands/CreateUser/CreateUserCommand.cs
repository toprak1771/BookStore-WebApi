using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommand
    {
        public CreateUserModel Model {get; set;}
        private readonly IMapper _mapper;
        private readonly IBookStoreDbContext _dbcontext;
        public CreateUserCommand(IBookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext=dbcontext;
            _mapper=mapper;
        }

        public void Handle()
        {
            
            var user=_dbcontext.Users.SingleOrDefault(x=>x.Email==Model.Email);
            if(user!=null){
                throw new InvalidOperationException("Kullanıcı zaten mevcut.");
            }

            user = _mapper.Map<User>(Model);
          
            _dbcontext.Users.Add(user);
            _dbcontext.SaveChanges();
        }

        public class CreateUserModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email {get; set;}
            public string Password { get; set; }
            
        }
    }
}