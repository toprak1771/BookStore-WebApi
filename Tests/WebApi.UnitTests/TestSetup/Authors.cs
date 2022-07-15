using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
             context.Authors.AddRange(
                new Author { Name="Eric", Surname="Ries", BornDate=new DateTime(1978,10,22) },
                new Author { Name="Charlotte", Surname="Gilman", BornDate=new DateTime(1860,03,07) },
                new Author { Name="Frank", Surname="Herbet", BornDate=new DateTime(1920,11,08) }
                );
        }
    }    
}