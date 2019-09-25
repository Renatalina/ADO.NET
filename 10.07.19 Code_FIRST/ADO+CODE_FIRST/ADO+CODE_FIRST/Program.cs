using ADO_CODE_FIRST.Helpers;
using ADO_CODE_FIRST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ADO_CODE_FIRST
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            { 
                using(LibraryContext context=new LibraryContext())
                {
                    if(context.Database.Exists()==false)
                    {
                        Country country = new Country
                        {
                            CountryName = "Ukraine"
                        };
                        Author author = new Author
                        {
                            LastName = "Lanskaya",
                            FirstName = "Natalina"
                        };
                        Book book = new Book
                        {
                            Title = "Proud and Money"
                        };
                        Publisher publisher = new Publisher
                        {
                            PublisherName = "Exotic"
                        };
                        //присоединили книгу для автора 
                        author.Books.Add(book);
                        //записали в Базу Данну нашего автора
                        context.Authors.Add(author);
                        country.Authors.Add(author);

                        context.Countries.AddRange(
                            new List<Country>
                            {
                                country,
                                new Country  {CountryName="Litva"}
                            });

                        publisher.Books.Add(book);
                        context.Publishers.Add(publisher);

                        /*
                        //это мы все отправили в базу данных! 
                        // без этой строки ничего не зайдет в Базу данных
                        context.SaveChanges();
                        */

                        //второй вариант
                        context.Books.Add(
                            new Book
                            {
                                Title = "YYY",
                                Publishers = new List<Publisher>
                                {
                                    new Publisher{ PublisherName="Ranok"},
                                    publisher
                                }
                            });
                        context.SaveChanges();

                        context.Authors.Add(
                            new Author
                            {
                                LastName = "Moore",
                                FirstName = "Oliver",
                                Country = context.Countries.First(c => c.CountryName == "Litva")
                            }
                            );

                        context.SaveChanges();
                        Author author1 = context.Authors.First(a => a.LastName == "Moore" && a.FirstName == "Oliver");
                        ((List<Book>)author1.Books).AddRange
                            (
                            new List<Book>
                            {
                                book,
                                context.Books.First(b => b.Title == "YYY")
                            });

                        context.SaveChanges();


                    }
                    if (context.Database.Exists())
                    {
                        foreach (Book book in context.Books.Include(b => b.Publishers).Include(b => b.Authors).ToList())
                        {
                            Console.Write(new string('\t', 2));
                            Console.WriteLine(book.Title);

                            foreach (Author author in book.Authors)
                            {
                                Console.Write(new string('\t', 3));

                                context.Entry(author).Reference("Country").Load();

                                Console.WriteLine(author + " " + author.Country.CountryName);
                            }

                            foreach (Publisher publ in book.Publishers)
                            {
                                Console.Write(new string('\t', 4));
                                Console.WriteLine(publ.PublisherName);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
