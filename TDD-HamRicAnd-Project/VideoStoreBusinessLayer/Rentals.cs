using System;
using System.Collections.Generic;
using System.Linq;

namespace VideoStoreBusinessLayer
{
    public class Rentals : IRentals
    {
        private List<Rental> _rentals { get; set; }
        private IDateTime _dateTime;
        public Rentals(IDateTime _dateTime)
        {
            this._dateTime = _dateTime;
            _rentals = new List<Rental>();
        }
        public void AddRental(string movieTitle, string socialSecurityNumber)
        {
            ValidateRental(movieTitle, socialSecurityNumber);
            _rentals.Add(new Rental(_dateTime.Now().AddDays(3),movieTitle, socialSecurityNumber));
        }

        private void ValidateRental(string movieTitle, string socialSecurityNumber)
        {
            var rentals = _rentals.Where(x => x._customerSsn == socialSecurityNumber).ToList();
           if (rentals.Count()>= 3)
            {
                throw new RentalOverloadExeption(ExeptionMessages.RentalOverloadExeptionMessage);
            }
            foreach (var rental in rentals)
            {
                if (rental._movieTitle == movieTitle)
                    throw new ForbidenRentalExeption(ExeptionMessages.ForbidenRentalExeptionMessage);
                if (rental.IsLate())
                    throw new LateRentalExeption(ExeptionMessages.LateRentalExeptionMessage);
            }
         
        }

        public List<Rental> GetRentalsFor(string socialSecurityNumber)
        {
            return _rentals.Where(x=> x._customerSsn == socialSecurityNumber).ToList();
        }

        public void RemoveRental(string movieTitle, string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }
    }
}