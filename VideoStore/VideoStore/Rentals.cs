using System;
using System.Collections.Generic;

namespace VideoStore
{
    public class Rentals : IRentals
    {
        IDateTime date;
        public Rentals(IDateTime date)
        {
            this.date = date;
        }
        public void AddRental(string movieTitle, string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }

        public List<Rental> GetRentalsFor(string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }

        public void RemoveRental(string movieTitle, string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }
    }
}