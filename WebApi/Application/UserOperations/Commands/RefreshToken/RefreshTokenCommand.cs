using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken {get; set;}
        private readonly IBookStoreDbContext _dbcontext;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommand (IBookStoreDbContext dbcontext, IConfiguration configuration)
        {
            _dbcontext=dbcontext;
            _configuration = configuration;
        }

        public Token Handle()
        {
            
            
            var user = _dbcontext.Users.FirstOrDefault(x=>x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
            if(user is not null)
            {
                
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

                _dbcontext.SaveChanges();

                return token;
            }
           else{
            throw new InvalidOperationException("Valid bir refresh token bulunamadı!");
           }
        }

    }
}