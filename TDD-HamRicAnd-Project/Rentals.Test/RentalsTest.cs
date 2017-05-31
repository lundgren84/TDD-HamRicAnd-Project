using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStoreBusinessLayer;

namespace Rental.Test
{
    [TestFixture]
    public class RentalTests
    {
        private IRentals sut;
        private IDateTime dateTime;
        private Customer TestCustomer;
        private Movie TestMovie;
        [SetUp]
        public void Setup()
        {

            dateTime = Substitute.For<IDateTime>();
            dateTime.Now().Returns(DateTime.Now);
            sut = new Rentals(dateTime);
            TestMovie = new Movie("Transporter", MovieGenre.Action);
            TestCustomer = new Customer("Olle Svensson", "1978-06-14");
        }
        [Test]
        public void TrueIfAbleToAddRental()
        {
            sut.AddRental(TestMovie.Title, TestCustomer.SSN);
            var rentals = sut.GetRentalsFor(TestCustomer.SSN);
            Assert.AreEqual(1, rentals.Count);
        }
        [Test]
        public void RentalsShudGetThreeDayLaterDueDate()
        {
            sut.AddRental(TestMovie.Title, TestCustomer.SSN);
            dateTime.Now().Returns(DateTime.Now.AddDays(3));
            var dateTest = dateTime.Now();


            var rentals = sut.GetRentalsFor(TestCustomer.SSN);

            Assert.AreEqual(dateTest.Year, rentals[0]._dueDate.Year);
            Assert.AreEqual(dateTest.Month, rentals[0]._dueDate.Month);
            Assert.AreEqual(dateTest.Day, rentals[0]._dueDate.Day);
        }
        [Test]
        public void GetRentalsBySocialSecurityNumber()
        {
            sut.AddRental(TestMovie.Title, TestCustomer.SSN);
            var rentals = sut.GetRentalsFor(TestCustomer.SSN);
            Assert.AreEqual(1, rentals.Count);
        }
        [Test]
        public void CustomerCanRentMoreThenOneMovie()
        {
            sut.AddRental("Die hard", TestCustomer.SSN);
            sut.AddRental("Die hard2", TestCustomer.SSN);
            var rentals = sut.GetRentalsFor(TestCustomer.SSN);
            Assert.AreEqual(2, rentals.Count);
        }
        [Test]
        public void ThrowsIfCustomerTryRentMoreThenThreeMovies()
        {
            sut.AddRental("Die hard", TestCustomer.SSN);
            sut.AddRental("Die hard2", TestCustomer.SSN);
            sut.AddRental("Die hard3", TestCustomer.SSN);
            Assert.Throws<RentalOverloadExeption>(() =>
            {
                sut.AddRental("Die hard4", TestCustomer.SSN);
            });
        }
        [Test]
        public void ThrowsIfRentSameTitletwice()
        {
            sut.AddRental("Die hard", TestCustomer.SSN);
            Assert.Throws<ForbidenRentalExeption>(() =>
            {
                sut.AddRental("Die hard", TestCustomer.SSN);
            });
        }
        [Test]
        public void ThrowsIfTryRentWhenHaveLateDueDateRental()
        {
            sut.AddRental("Die hard", TestCustomer.SSN);
            var rentals = sut.GetRentalsFor(TestCustomer.SSN);

            dateTime.Now().Returns(DateTime.Now.AddDays(-4));

            rentals[0]._dueDate = dateTime.Now();

            Assert.Throws<LateRentalExeption>(() =>
            {
                sut.AddRental("Die hard2", TestCustomer.SSN);
            });
        }
        [Test]
        public void TrueIfAbleToReturnMovie()
        {
            sut.AddRental("Die hard", TestCustomer.SSN);
            sut.RemoveRental(TestMovie.Title, TestCustomer.SSN);
            var rentals = sut.GetRentalsFor(TestCustomer.SSN);
            Assert.AreEqual(0, rentals.Count);
        }
      
    }
}
