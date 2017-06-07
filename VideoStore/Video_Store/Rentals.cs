 using System;
using System.Collections.Generic;
using System.Linq;

namespace Video_Store
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
            var tooLateRented = GetRentalsFor(socialSecurityNumber).Where(x => x.DayReturn <= date.Now()).ToList();
            if (tooLateRented.Any())            
                throw new RentalException(tooLateRented);            
            if (GetRentalsFor(socialSecurityNumber).Count >= 3)
                throw new RentalException("Max 3 movies!");
            if (GetRentalsFor(socialSecurityNumber).Exists(x => x.Movie == movieTitle))
                throw new RentalException("You have in your possession  this movie");

            var rental = new Rental { Customer = socialSecurityNumber, Movie = movieTitle, DayRented = date.Now() };
            rented.Add(rental);
        }

        public List<Rental> GetRentalsFor(string socialSecurityNumber)
        {
            return rented.Where(x => x.Customer.Equals(socialSecurityNumber)).ToList();
        }

        public void RemoveRental(string movieTitle, string socialSecurityNumber)
        {
            var RentaltoRemove = GetRentalsFor(socialSecurityNumber).FirstOrDefault(x => x.Movie == movieTitle);

            rented.Remove(RentaltoRemove);
        }
    }
}