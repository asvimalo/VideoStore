using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Video_Store;
using NSubstitute;


namespace VideoStore.Tests
{
    [TestFixture]
    public class VideoStoreTests
    {
        private IRentals rentals;
        private RealVideoStore sut;
        private Movie movie;

        [SetUp]
        public void SetUp()
        {
            rentals = Substitute.For<Rentals>(Substitute.For<IDateTime>());
            sut = new RealVideoStore(rentals);
            movie = new Movie { Title = "Rambo", Year = 2001, Genre = "Porn" };
        }
        [Test]
        public void IncorrectSsnFormatThrowsException()
        {
            Assert.Throws<SSNFormatException>(() =>
                sut.RegisterCustomer("John Doe", "20000101")
            );
            Assert.Throws<SSNFormatException>(() =>
                sut.RentMovie("Rambo", "20000101")
            );
            Assert.Throws<SSNFormatException>(() =>
                sut.ReturnMovie("Rambo", "20000101")
            );
        }

        [Test]
        public void MovieTitleIfEmptyThrowsException()
        {
            sut.RegisterCustomer("John Doe", "2000-01-01");

            Assert.Throws<MovieException>(() =>
                sut.AddMovie(new Movie
                {
                    Title = string.Empty,
                    Year = 1988,
                    Genre = "Action"
                })
            );
            Assert.Throws<MovieException>(() =>
                sut.RentMovie(string.Empty, "2000-01-01")
            );
            Assert.Throws<MovieException>(() =>
                sut.ReturnMovie(string.Empty, "2000-01-01")
            );
        }



        

        [Test]
        public void CanRegisterANewCustomer()
        {
            sut.RegisterCustomer("John Doe", "2000-01-01");
            var customers = sut.GetCustomers();

            Assert.AreEqual(1, customers.Count);
            Assert.AreEqual("John Doe", customers[0].Name);
        }

        [Test]
        public void NameCantBeEmpty()
        {
            Assert.Throws<CustomerException>(() =>
                sut.RegisterCustomer(string.Empty, "2000-01-01")
            );
        }

        [Test]
        public void CantRegisterSameCustomer()
        {
            sut.RegisterCustomer("Dalius", "1920-01-01");

            Assert.Throws<CustomerException>(() =>
                sut.RegisterCustomer("Dalius", "1920-01-01")
            );
        }
        

        [Test]
        public void CanAddMovie()
        {
            sut.AddMovie(movie);
            var movies = sut.GetMovies();

            Assert.AreEqual(1, movies.Count);
            Assert.Contains(movie, movies);
        }

        [Test]
        public void CanAddSameMovieUpToThreeTimes()
        {
            sut.AddMovie(movie);
            sut.AddMovie(movie);
            sut.AddMovie(movie);
            var movies = sut.GetMovies();

            Assert.AreEqual(3, movies.Count);
        }

        [Test]
        public void AddingMoreThanThreeSameMovieThrowsException()
        {
            sut.AddMovie(movie);
            sut.AddMovie(movie);
            sut.AddMovie(movie);

            Assert.Throws<MovieException>(() =>
                sut.AddMovie(movie)
            );
        }


        [Test]
        public void CanRentMovie()
        {
            sut.RegisterCustomer("John Doe", "2000-01-01");
            sut.AddMovie(movie);
            sut.RentMovie("Rambo", "2000-01-01");

            rentals.Received().AddRental(Arg.Is("Rambo"), Arg.Is("2000-01-01"));
        }

        [Test]
        public void CannotRentNonExistentMovie()
        {
            sut.RegisterCustomer("John Doe", "2000-01-01");

            var exception = Assert.Throws<MovieException>(() =>
                sut.RentMovie("Rambo", "2000-01-01")
            );
            Assert.AreEqual("Movie doesn't exit", exception.Message);
            rentals.DidNotReceive().AddRental(Arg.Is("Rambo"), Arg.Is("2000-01-01"));
        }

        [Test]
        public void UnregisteredCustomerCannotRentMovie()
        {
            sut.AddMovie(movie);

            Assert.Throws<CustomerException>(() =>
                sut.RentMovie("Rambo", "2000-01-01")
            );
            rentals.DidNotReceive().AddRental(Arg.Is("Rambo"), Arg.Is("2000-01-01"));
        }



        [Test]
        public void CanReturnRental()
        {
            sut.RegisterCustomer("Dalius", "1920-01-01");
            sut.AddMovie(movie);
            sut.RentMovie("Rambo", "1920-01-01");

            sut.ReturnMovie("Rambo", "1920-01-01");

            rentals.Received().RemoveRental("Rambo", "1920-01-01");
            Assert.AreEqual(0, rentals.GetRentalsFor("1920-01-01").Count);
        }

        [Test]
        public void CantReturnNonRentedMovie()
        {
            sut.RegisterCustomer("Dalius", "2000-01-01");
            sut.AddMovie(movie);

            Assert.Throws<RentalException>(() =>
                sut.ReturnMovie("Rambo", "2000-01-01")
            );
        }
    }


}

