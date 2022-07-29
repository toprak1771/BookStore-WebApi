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
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model {get; set;}
        private readonly IMapper _mapper;
        private readonly IBookStoreDbContext _dbcontext;
        private readonly IConfiguration _configuration;

        public CreateTokenCommand(IBookStoreDbContext dbcontext, IMapper mapper, IConfiguration configuration)
        {
            _dbcontext=dbcontext;
            _mapper=mapper;
            _configuration = configuration;
        }

        public Token Handle()
        {
            
            //İlk olarak token yaratmak istediğimiz kullanıcı sisteme kayıtlı mı dşye kontrol etmemiz gerekir, sisteme kayıtlı değilse token oluşturamayız.
            var user = _dbcontext.Users.FirstOrDefault(x=>x.Email == Model.Email && x.Password == Model.Password);
            if(user is not null)
            {
                //Create Token
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

                _dbcontext.SaveChanges();

                return token;
            }
           else{
            throw new InvalidOperationException("Kullanıcı adı-parola hatalı. ");
           }
        }

        public class CreateTokenModel
        {

            public string Email { get; set; }
            public string Password { get; set; }
        }

    }
}