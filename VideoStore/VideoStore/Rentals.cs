using System;
using System.Collections.Generic;
using System.Linq;

namespace VideoStore
{
    public class Rentals : IRentals
    {
        IDateTime date;
        public List<Rental> rented;
        public Rentals(IDateTime date)
        {
            this.date = date;
            this.rented = new List<Rental>(); 
        }
        public void AddRental(string movieTitle, string socialSecurityNumber)
        {
            var rental = new Rental { Customer = socialSecurityNumber, Movie = movieTitle }
        }

        public List<Rental> GetRentalsFor(string socialSecurityNumber)
        {
            return rented.Where(x => x.Customer.Equals(socialSecurityNumber)).ToList();
        }

        public void RemoveRental(string movieTitle, string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }
    }
}