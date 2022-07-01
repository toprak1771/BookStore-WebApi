using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;


namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context=new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books==null)
                {
                    return;
                }
                
                context.Genres.AddRange(
                    new Genre
                    {
                         Name="Personal Growth"   
                    },
                     new Genre
                    {
                         Name="Science Fiction"   
                    },
                     new Genre
                    {
                         Name="Novel"   
                    }
                );

                context.Authors.AddRange(
                    new Author
                    {
                        Name="Eric",
                        Surname="Ries",
                        BornDate=new DateTime(1978,10,22)
                    },
                      new Author
                    {
                        Name="Charlotte",
                        Surname="Gilman",
                        BornDate=new DateTime(1860,03,07)
                    },
                      new Author
                    {
                        Name="Frank",
                        Surname="Herbet",
                        BornDate=new DateTime(1920,11,08)
                    }
                );
                

                context.Books.AddRange(
                new Book      
                {
                //Id=1,
                Title="Learn Startup",
                GenreId=1, //Personal Growth
                PageCount=200,
                PublishDate=new DateTime(2001,06,12),
                AuthorId=1
                },
                new Book
                {
                //Id=2,
                Title="Herland",
                GenreId=2, //Science fiction
                PageCount=250,
                PublishDate=new DateTime(2010,05,23),
                AuthorId=2
                },
              new Book
                {
                //Id=3,
                Title="Dune",
                GenreId=3, //Personal Fiction
                PageCount=540,
                PublishDate=new DateTime(2001,12,21),
                AuthorId=3
                }
                );
                context.SaveChanges();
            }
        }       
        
    }
}