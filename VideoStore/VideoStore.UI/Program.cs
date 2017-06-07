using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Video_Store;

namespace VideoStore.UI
{
    class Program
    {
        static void PrintMenu()
        {
            Console.WriteLine("::::::::::::::::::::::  ::::               ::::");
            Console.WriteLine("::::::::::::::::::::::  ::::::             ::::::");
            Console.WriteLine("        ::::            ::::  ::           ::::  ::");
            Console.WriteLine("        ::::            ::::    ::         ::::    ::");
            Console.WriteLine("        ::::            ::::      ::       ::::      ::");
            Console.WriteLine("        ::::            ::::      ::       ::::      ::");
            Console.WriteLine("        ::::            ::::      ::       ::::      ::");
            Console.WriteLine("        ::::            ::::      ::       ::::      ::");
            Console.WriteLine("        ::::            ::::     ::        ::::     ::");
            Console.WriteLine("        ::::            ::::   ::          ::::   ::");
            Console.WriteLine("        ::::            ::::::             ::::::");
            Console.WriteLine("        ::::            ::::               ::::");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("               1. Register Customer");
            Console.WriteLine("               2. Add Movie");
            Console.WriteLine("               3. Rent Movie");
            Console.WriteLine("               4. Return Movie");
            Console.WriteLine("               5. Movies");
            Console.WriteLine("               6. Exit");
        }
        static void Main(string[] args)
        {
            var store = new RealVideoStore(new Rentals(new Video_Store.DateTime()));
            var Exit = false;
            do
            {
                Console.Clear();
                PrintMenu();

                Console.Write("               > ");
                var choice = Console.ReadLine();

                switch (choice.ToLower())
                {
                    case "1":
                        RegisterCustomer(store);
                        break;
                    case "2":
                        AddMovie(store);
                        break;
                    case "3":
                        RentAMovie(store);
                        break;
                    case "4":
                        ReturnMovie(store);
                        break;
                    case "5":
                        GetMovies(store);
                        break;
                    case "6":
                        Exit = true;
                        break;
                }
                Console.ReadLine();
            } while (!Exit);
        }
        private static void RegisterCustomer(IVideoStore store)
        {
            Console.Write("Enter your name: ");
            var name = Console.ReadLine();
            Console.Write("Enter SSN: ");
            var ssn = Console.ReadLine();

            try
            {
                store.RegisterCustomer(name, ssn);
                
            }
            catch (SSNFormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (CustomerException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void AddMovie(IVideoStore store)
        {
            int year = 0;
            //string genre = null;
            var movie = new Movie();
            Console.Write("Enter movie title: ");
            movie.Title = Console.ReadLine();
            Console.Write("Production year: ");
            movie.Year = int.TryParse(Console.ReadLine(), out  year) ? year : 2017;
            Console.Write("Enter movie genre: ");
            movie.Genre = Console.ReadLine();

            try
            {
                store.AddMovie(movie);
               
            }
            catch (MovieException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void RentAMovie(IVideoStore store)
        {
            Console.Write("Enter title: ");
            var title = Console.ReadLine();
            Console.WriteLine("Enter social security number: ");
            var ssn = Console.ReadLine();

            try
            {
                store.RentMovie(title, ssn);
                Console.WriteLine($"{title} rented");
            }
            catch (SSNFormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (MovieException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (CustomerException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (RentalException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void ReturnMovie(IVideoStore store)
        {
            Console.Write("Enter movie title: ");
            var title = Console.ReadLine();
            Console.Write("Enter SSN: ");
            var ssn = Console.ReadLine();

            try
            {
                store.ReturnMovie(title, ssn);
                Console.WriteLine($"{title} received");
            }
            catch (SSNFormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (RentalException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void GetMovies(IVideoStore store)
        {
            
            foreach (var movie in store.GetMovies())
            {
                Console.WriteLine($"{movie.Title} - {movie.Year} - {movie.Genre}"); 
            }

            
        }
       
    }
}
