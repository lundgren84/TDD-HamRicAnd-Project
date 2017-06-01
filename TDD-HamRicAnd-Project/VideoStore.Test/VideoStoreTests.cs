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
            TestCustomer = new Customer("1978-06-14","Olle Svensson" );
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
            sut.RegisterCustomer(TestCustomer.SSN, TestCustomer.Name);
            var customer = sut.GetCustomers().FirstOrDefault(x => x.SSN == TestCustomer.SSN);

            Assert.IsNotNull(customer);
        }
        [Test]
        public void ThrowExeptionIfAddExisitingUser()
        {
            sut.RegisterCustomer( TestCustomer.SSN, TestCustomer.Name);

            Assert.Throws<CustomerExistsExeption>(() =>
            {
                sut.RegisterCustomer(TestCustomer.SSN, TestCustomer.Name);
            });
            ;
        }
        [Test]
        public void ThrowIfRegisterCustomerWithoutName()
        {
            Assert.Throws<NameNullOrEmptyExeption>(() => {


                sut.RegisterCustomer(TestCustomer.SSN, "");
            });
        }
        [Test]
        public void ThrowExeptionIfInvalidSocialSecurityNumber()
        {
            Assert.Throws<InvalidSocialSecurityNumberExeption>(() =>
            {
                sut.RegisterCustomer("74747474", TestCustomer.Name);
            
            });
        }
        [Test]
        public void ThrowExeptionIfRentNonExistingMovie()
        {
            sut.RegisterCustomer(TestCustomer.SSN, TestCustomer.Name);
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
            sut.RegisterCustomer(TestCustomer.SSN, TestCustomer.Name);
            sut.RentMovie(TestMovie.Title, TestCustomer.SSN);
            rentals.Received(1).AddRental(Arg.Any<string>(), Arg.Any<string>());
        }
        [Test]
        public void ThrowsExeptionIfNotRegisterdCustomerTryReturnMovie()
        {
            Assert.Throws<CustomerDontExistsExeption>(() =>
            {
                sut.ReturnMovie("DieHard", TestCustomer.SSN);
            });         
        }
       
         [Test]
        public void ThrowsExeptionIfTryReturnMovieYouDontRent()
        {
            sut.RegisterCustomer(TestCustomer.SSN, TestCustomer.Name);
            Assert.Throws<MovieDontExistsExeption>(() =>
            {
                sut.ReturnMovie("Olles film om löv3", TestCustomer.SSN);
            });

        }
        [Test]
        public void TrueIfIRentalsRunsRemoveRentalWhenReturnMovie()
        {
            rentals.GetRentalsFor(Arg.Any<string>()).Returns(new List<Rental>() {new Rental(DateTime.Now,TestMovie.Title,TestCustomer.SSN )});

            AddCustomer_AddMovie();
            sut.RentMovie(TestMovie.Title, TestCustomer.SSN);

            sut.ReturnMovie(TestMovie.Title, TestCustomer.SSN);

            rentals.Received(1).RemoveRental(Arg.Any<string>(),Arg.Any<string>());
        }
        [Test]
        public void ThrowsIfReturningMovieWithLateDueDate()
        {
            rentals.GetRentalsFor(Arg.Any<string>()).Returns(new List<Rental>() { new Rental(DateTime.Now, TestMovie.Title, TestCustomer.SSN) });
       

            AddCustomer_AddMovie();
            sut.RentMovie(TestMovie.Title, TestCustomer.SSN);


            Assert.Throws<LateRentalExeption>(() =>
            {
                sut.ReturnMovie(TestMovie.Title, TestCustomer.SSN);
            });
        }



        public void AddCustomer_AddMovie()
        {
            sut.RegisterCustomer(TestCustomer.SSN, TestCustomer.Name);

            sut.AddMovie(TestMovie);

         
        }
    }
}
