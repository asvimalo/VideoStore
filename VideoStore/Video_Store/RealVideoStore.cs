using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Video_Store
{
    public class RealVideoStore : IVideoStore
    {
        private IRentals Rentals;
        private List<Customer> Customers;
        private Dictionary<string, List<Movie>> Movies;

        public RealVideoStore(IRentals rentals)
        {
            Rentals = rentals;
            Customers = new List<Customer>();
            Movies = new Dictionary<string, List<Movie>>();
        }

        public void AddMovie(Movie movie)
        {
            if (string.IsNullOrEmpty(movie.Title))
                throw new MovieException("Enter a movie name");
            if (movie != null && Movies.ContainsKey(movie.Title))
            {
                if (Movies[movie.Title].Count(x => x.Title.Equals(movie.Title)) >= 3)
                    throw new MovieException("No more than three same movie");

                Movies[movie.Title].Add(movie);
            }
            else
                Movies[movie.Title] = new List<Movie> {movie};
                
        }

        

        public List<Customer> GetCustomers()
        {
            return Customers;
        }

        public void RegisterCustomer(string name, string socialSecurityNumber)
        {
            if (string.IsNullOrEmpty(name))
                throw new CustomerException("Name can't be empty");
            checkSsnFormat(socialSecurityNumber);
            if (Customers.Exists(x => x.Name.Equals(name) && x.Ssn.Equals(socialSecurityNumber)))
                throw new CustomerException("Customer exists");
            Customers.Add(new Customer { Name = name, Ssn = socialSecurityNumber });
        }

        static void checkSsnFormat(string ssn)
        {
            if (!Regex.IsMatch(ssn, @"^\d{4}-((0\d)|(1[012]))-(([012]\d)|3[01])$"))
                throw new SSNFormatException(ssn);
        }

        public void RentMovie(string movieTitle, string socialSecurityNumber)
        {
            if (string.IsNullOrEmpty(movieTitle))
                throw new MovieException("Movie title is empty");
            checkSsnFormat(socialSecurityNumber);
            if (!Customers.Any(x => x.Ssn.Equals(socialSecurityNumber)))
                throw new CustomerException("Need to be registered");
            if (!Movies.ContainsKey(movieTitle))
                throw new MovieException("Movie doesn't exit");

            Rentals.AddRental(movieTitle, socialSecurityNumber);
        }
        public List<Movie> GetMovies()
        {
            
            return Movies.SelectMany(x => x.Value).ToList();
        }
        public void ReturnMovie(string movieTitle, string socialSecurityNumber)
        {
            if (string.IsNullOrEmpty(movieTitle))
                throw new MovieException("Movie title is empty");
            checkSsnFormat(socialSecurityNumber);
            if (!Rentals.GetRentalsFor(socialSecurityNumber).Any(x => x.Movie.Equals(movieTitle)))
                throw new RentalException("Need to be registered");
            if (!Movies.ContainsKey(movieTitle))
                throw new MovieException("Movie doesn't exit");

            Rentals.RemoveRental(movieTitle, socialSecurityNumber);
        }
    }
}
