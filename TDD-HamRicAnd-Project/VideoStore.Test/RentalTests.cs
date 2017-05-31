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
    public class RentalTests
    {
        private IRentals sut;
        private IDateTime dateTime;
        [SetUp]
        public void Setup()
        {
            dateTime = Substitute.For<IDateTime>();
            dateTime.Now().Returns(DateTime.Now);
              sut = new Rentals(dateTime);
        }
        [Test]
        public void TrueIfAbleToAddRental()
        {
            sut.AddRental("Die hard", "19880606");
            var rentals = sut.GetRentalsFor("19880606");
            Assert.AreEqual(1, rentals.Count);
        }
        [Test]
        public void RentalsShudGetThreeDayLaterDueDate()
        {
            sut.AddRental("Die hard", "19880606");
            dateTime.Now().Returns(DateTime.Now.AddDays(3));
            var dateTest = dateTime.Now();

            
            var rentals = sut.GetRentalsFor("19880606");

            Assert.AreEqual(dateTest.Year, rentals[0]._dueDate.Year);
            Assert.AreEqual(dateTest.Month, rentals[0]._dueDate.Month);
            Assert.AreEqual(dateTest.Day, rentals[0]._dueDate.Day);
        }
        [Test]
        public void GetRentalsBySocialSecurityNumber()
        {
            sut.AddRental("Die hard", "19880606");
            var rentals = sut.GetRentalsFor("19880606");
            Assert.AreEqual(1, rentals.Count);
        }
        [Test]
        public void CustomerCanRentMoreThenOneMovie()
        {
            sut.AddRental("Die hard", "19880606");
            sut.AddRental("Die hard2", "19880606");
            var rentals = sut.GetRentalsFor("19880606");
            Assert.AreEqual(2, rentals.Count);
        }
        [Test]
        public void ThrowsIfCustomerTryRentMoreThenThreeMovies()
        {
            sut.AddRental("Die hard", "19880606");
            sut.AddRental("Die hard2", "19880606");
            sut.AddRental("Die hard3", "19880606");
            Assert.Throws<RentalOverloadExeption>(() =>
            {
                sut.AddRental("Die hard4", "19880606");
            });
        }
        [Test]
        public void ThrowsIfRentSameTitletwice()
        {
            sut.AddRental("Die hard", "19880606");
            Assert.Throws<ForbidenRentalExeption>(() =>
            {
                sut.AddRental("Die hard", "19880606");
            });
        }
        [Test]
        public void ThrowsIfTryRentWhenHaveLateDueDateRental()
        {
            sut.AddRental("Die hard", "19880606");
            var rentals = sut.GetRentalsFor("19880606");

            dateTime.Now().Returns(DateTime.Now.AddDays(-4));

            rentals[0]._dueDate = dateTime.Now();
           
            Assert.Throws<LateRentalExeption>(() =>
            {
                sut.AddRental("Die hard2", "19880606");
            });
        }
        //[Test]
        public void TrueIfAbleToReturnMovie()
        {
            Assert.AreEqual(1, 2);
        }
        //[Test]
        public void ThrowsExeptionIfTryReturnMovieYouDontRent()
        {
            Assert.AreEqual(1, 2);
        }
    }
}
