using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore
{
    public class VideoStore : IVideoStore
    {
        private IRentals Rentals;
        private List<Movie> Movies;
        private Dictionary<string, Movie> Rented;
        public VideoStore(IRentals rentals)
        {
            Rentals = rentals;
            Movies = new List<Movie>();
            Rented = new Dictionary<string, Movie>();
        }

        public void AddMovie(Movie movie)
        {
            if (movie != null)
            {
                Movies.Add(movie);
            }
            else
                throw new MovieException();
                
        }

        private Exception MovieException()
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public void RegisterCustomer(string name, string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }

        public void RentMovie(string movieTitle, string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }

        public void ReturnMovie(string movieTitle, string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }
    }
}
