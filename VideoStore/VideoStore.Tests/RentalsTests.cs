using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Video_Store;

namespace VideoStore.Tests
{
    [TestFixture]
    public class RentalsTests
    {
        private Rentals sut;
        private IDateTime date;

        [SetUp]
        public void Setup()
        {
            date = Substitute.For<IDateTime>();
            sut = new Rentals(date);

            date.Now().Returns(new System.DateTime(1222, 12, 12));
        }

        [Test]
        public void CanAddARental()
        {
            string title = "Jesus super star";
            string socialNumber = "1979-01-01";

            sut.AddRental(title, socialNumber);

            Assert.AreEqual(1, sut.GetRentalsFor(socialNumber).Count);
        }

        [Test]
        public void CanRemoveRental()
        {
            sut.AddRental("Rambo", "2000-01-01");

            sut.RemoveRental("Rambo", "2000-01-01");

            Assert.AreEqual(0, sut.GetRentalsFor("2000-01-01").Count);
        }

        [Test]
        public void CannotRentMovieWithPendingDate()
        {
            date.Now().Returns(new System.DateTime(2000, 1, 1));

            sut.AddRental("Amor a quema ropa", "1979-01-01");
            date.Now().Returns(new System.DateTime(2000, 1, 4));

            Assert.Throws<RentalException>(() => sut.AddRental("Rambo", "1979-01-01"));
        }

        [Test]
        public void GetRentalsBySSN()
        {
            sut.AddRental("The 101 Dalmatas", "1979-01-01");
            var result = sut.GetRentalsFor("1979-01-01");
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void CanRentmoreThanOneMovie()
        {
            sut.AddRental("The 101 Dalmatas", "1979-01-01");
            sut.AddRental("Amor a quema ropa", "1979-01-01");
            sut.AddRental("Dalius drinking mojitos", "1979-01-01");

            var result = sut.GetRentalsFor("1979-01-01");
            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public void MaxRentedMoviesAllowed()
        {

            sut.AddRental("The 101 Dalmatas", "1979-01-01");
            sut.AddRental("Amor a quema ropa", "1979-01-01");
            sut.AddRental("Dalius drinking mojitos", "1979-01-01");

            Assert.Throws<RentalException>(() => sut.AddRental("Birds", "1979-01-01"));
        }

        [Test]
        public void CustomerCantRentSameMovieTwice()
        {
            sut.AddRental("The 101 Dalmatas", "1979-01-01");
            Assert.Throws<RentalException>(() => sut.AddRental("The 101 Dalmatas", "1979-01-01"));
        }

        [Test]
        public void CustomerCantRentNewMoviesWithPendingReturns()
        {
            date.Now().Returns(new System.DateTime(2000, 1, 1));
            sut.AddRental("Batman v superman", "2000-01-01");
            date.Now().Returns(new System.DateTime(2000, 1, 4));

            Assert.Throws<RentalException>(() => sut.AddRental("Rambo", "2000-01-01"));
        }


    }
}
