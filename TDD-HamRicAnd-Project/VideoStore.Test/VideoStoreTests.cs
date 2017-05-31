using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStoreBusinessLayer;

namespace VideoStoreTest
{
    [TestFixture]
    public class VideoStoreTests
    {
        private IVideoStore sut;
        private IRentals rentals;
        private Movie TestMovie;
        private Customer TestCustomer;
      

        [SetUp]
        public void Setup()
        {
            rentals = Substitute.For<IRentals>();
            sut = new VideoStore(rentals);
            TestMovie = new Movie("Transporter", MovieGenre.Action);
            TestCustomer = new Customer("Olle Svensson", "1978-06-14");
        }
        [Test]
        public void TrueIfCanAddNewMovieToStore()
        {
            sut.AddMovie(TestMovie);
            var movie = sut.Movies.Where(x => x.Title == "Transporter" && x.Genre == MovieGenre.Action);
            Assert.IsTrue(movie != null);
        }
        [Test]
        public void MovieTitleCanNotBeNullOrEmpty()
        {
            TestMovie.Title = "";
            Assert.Throws<MovieTitelsIsNullOrEmptyExeption>(() =>
            {
                sut.AddMovie(TestMovie);
            });
        }
        [Test]
        public void ThrowExeptionIfAddedFourOrMoreMoviesWithSameTitle()
        {
            sut.AddMovie(TestMovie);
            sut.AddMovie(TestMovie);
            sut.AddMovie(TestMovie);
            Assert.Throws<MovieTitleOverloadExeption>(() =>
            {
                sut.AddMovie(TestMovie);
            });
        }

        // Customer Tests
        [Test]
        public void CanRegisterCustomer()
        {
            sut.RegisterCustomer(TestCustomer.Name,TestCustomer.SSN);
            var customer = sut.GetCustomers().FirstOrDefault(x => x.SSN == TestCustomer.SSN);

            Assert.IsNotNull(customer);
        }
        [Test]
        public void ThrowExeptionIfAddExisitingUser()
        {
            sut.RegisterCustomer(TestCustomer.Name, TestCustomer.SSN);

            Assert.Throws<CustomerExistsExeption>(() =>
            {
                sut.RegisterCustomer(TestCustomer.Name, TestCustomer.SSN);
            });
            ;
        }
        [Test]
        public void ThrowExeptionIfInvalidSocialSecurityNumber()
        {
            Assert.Throws<InvalidSocialSecurityNumberExeption>(() =>
            {
                sut.RegisterCustomer(TestCustomer.Name, "19781027");
            });
        }
        [Test]
        public void ThrowExeptionIfRentNonExistingMovie()
        {
            sut.RegisterCustomer(TestCustomer.Name, TestCustomer.SSN);
            Assert.Throws<MovieDontExistsExeption>(() =>
            {
                sut.RentMovie("Olles film om havet", TestCustomer.SSN);
            });
        }
        [Test]
        public void ThrowExeptionNonExistingCustomerRentsMovie()
        {      
            Assert.Throws<CustomerDontExistsExeption>(() =>
            {
                sut.RentMovie(TestMovie.Title, TestCustomer.SSN);
            });
        }
        [Test]
      public void TrueIfIRentalsRunsAddRentalWhenRentMovie()
        {
            sut.AddMovie(TestMovie);
            sut.RegisterCustomer(TestCustomer.Name, TestCustomer.SSN);
            sut.RentMovie(TestMovie.Title, TestCustomer.SSN);
            rentals.Received(1).AddRental(Arg.Any<string>(), Arg.Any<string>());
        }
        //[Test]
        public void TrueIfIRentalsRunsRemoveRentalWhenReturnMovie()
        {
            Assert.AreEqual(1, 2);
        }
    }
}
